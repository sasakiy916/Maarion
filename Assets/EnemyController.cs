using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody rb;
    bool isAlive = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float x = -1;
        if (isAlive) rb.velocity = new Vector3(x, rb.velocity.y, 0);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (isAlive && other.collider.tag == "Player")
        {
            isAlive = false;
            rb.isKinematic = true;
            transform.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
            StartCoroutine(Death(2f));
        }
    }
    IEnumerator Death(float second)
    {
        yield return new WaitForSeconds(second);
        Destroy(this.gameObject);
    }
}
