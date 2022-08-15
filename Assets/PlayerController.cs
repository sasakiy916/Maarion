using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isJump = true;
    Rigidbody rb;
    public float walkForce;
    float maxSpeed = 10;
    public float jumpForce;
    public PhysicMaterial pm;
    public float friction = 1f;
    public float jumpPosY = 0;
    public float maxJumpPos = 10f;
    float gravity = -9.81f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float xSpeed = Input.GetAxis("Horizontal") * walkForce;
        // rb.AddForce(xSpeed, 0, 0);
        if (isJump == false && Input.GetButtonDown("Jump")) Jump();
        if(isJump && rb.velocity.y < 0)Physics.gravity = Physics.gravity * (1+(2f *Time.deltaTime));
        // rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y, 0);
        if(isJump)rb.AddForce(xSpeed,0,0);
        rb.velocity = new Vector3(xSpeed, rb.velocity.y, 0);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "GroundTop")
        {
            isJump = false;
            rb.mass = 1;
            pm.dynamicFriction = friction;
            pm.staticFriction = friction;
            Physics.gravity = new Vector3(0,gravity,0);
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
        jumpPosY = transform.position.y;
        pm.dynamicFriction = 0f;
        pm.staticFriction = 0f;
        rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        // rb.mass = 10f;
    }
}
