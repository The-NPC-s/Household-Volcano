using System.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public int sensitivity = 1;
    public int jumpForce = 10;
    public int speed = 5;
    public bool onGround = true;
    public bool dead = false;
    private Animator animator;

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

    private void Move()
    {
        if (!onGround){
            speed = 1;
        }
        else{
            speed = 5;
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
            GetComponent<Rigidbody>().AddRelativeForce(0, 0, 3*-speed/2);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(0, 0, 3*speed/2);
        }

        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
            animator.SetBool("isJumping", true);
        }
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * Input.GetAxis("Mouse X"));
    }

    private void Drag()
    {
        if (onGround)
            GetComponent<Rigidbody>().drag = 6f;
        else
            GetComponent<Rigidbody>().drag = 1.4f;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!onGround && other.collider.tag == "Ground")
        {
            onGround = true;
            animator.SetBool("isJumping", false);
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
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
