using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isJump;
    Rigidbody rb;
    public float walkForce;
    float maxSpeed = 10;
    public float jumpForce;
    public PhysicMaterial pm;
    public float friction = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(pm.dynamicFriction);
    }

    void Update()
    {
        float xSpeed = Input.GetAxis("Horizontal") * walkForce;
        if (isJump == false) rb.AddForce(xSpeed, 0, 0);
        // rb.AddForce(xSpeed, 0, 0);
        if (isJump == false && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y, 0);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "GroundTop")
        {
            isJump = false;
            pm.dynamicFriction = friction;
            pm.staticFriction = friction;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "GroundTop")
        {
            isJump = true;
        }
    }
    void Jump()
    {
        pm.dynamicFriction = 0f;
        pm.staticFriction = 0f;
        rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
    }
}
