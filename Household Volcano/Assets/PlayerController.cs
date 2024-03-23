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
        Move();
        Rotate();
        Drag();

        if(GetComponent<Rigidbody>().velocity.magnitude > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
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
        if (!onGround && other.collider.tag == "Ground"){
            onGround = true;
        }

        if (other.collider.tag == "lava")
        {
            GameObject.Find("puppet_kid").GetComponent<Renderer>().material.color = Color.black;
            // Turn the player all black
            GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
