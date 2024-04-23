using System.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public int sensitivity = 1;
    public int jumpForce = 10;
    public int base_speed=3;
    private int speed;
    public bool onGround = true;
    public bool dead = false;
    private Animator animator;
    private float distToGround = 1.33f;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            Move();
            Rotate();
            Drag();
            aniamte();
        }
        
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround);
    }

private void Move()
    {
        if (!IsGrounded())
        {
            speed = base_speed/5;
        }
        else{
            speed = base_speed;
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(speed, 0, 0);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(-speed, 0, 0);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(0, 0, -speed/1.5f);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(0, 0, speed/1.5f);
        }

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * Input.GetAxis("Mouse X"));
    }

    private void Drag()
    {
        if (IsGrounded())
            GetComponent<Rigidbody>().drag = 6f;
        else
            GetComponent<Rigidbody>().drag = 1.4f;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!IsGrounded())
        {
            
        }
    }

    //Tried to fix bug where you can walk off surface and then jump
    private void OnCollisionExit(Collision other)
    {
        /*
        if (onGround && other.collider.tag == "Ground")
        {
            onGround = false;
            animator.SetBool("isJumping", true);
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "lava")
        {
            GameObject puppet = GameObject.Find("puppet_kid");
            // Turn the player all black
            foreach (Renderer ren in puppet.GetComponentsInChildren<Renderer>())
            {
                ren.material.color = Color.black;
            }
            dead = true;
            puppet.GetComponent<Animator>().enabled = false;
            GameObject.Find("lava").GetComponent<LavaController>().lava_enabled = false;
        }
    }

    private void aniamte()
    {
        if (IsGrounded())
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
