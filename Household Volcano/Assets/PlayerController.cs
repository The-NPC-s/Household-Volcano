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
    // Start is called before the first frame update
    public bool onGround = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        Drag();
    }

    private void Move()
    {
        if (!onGround){
            speed = 1;
        }
        else{ //5 for speed
            speed = 5;
        }
        /*
        if (Input.GetAxis("Vertical") > 0 && (Input.GetAxis("Horizontal") > 0)) {
            GetComponent<Rigidbody>().AddRelativeForce(1, 0, 1);
        }
        else if (Input.GetAxis("Vertical") > 0 && (Input.GetAxis("Horizontal") < 0)) {
            GetComponent<Rigidbody>().AddRelativeForce(1, 0, 1);
        }
        else if (Input.GetAxis("Vertical") < 0 && (Input.GetAxis("Horizontal") > 0))
        {
            GetComponent<Rigidbody>().AddRelativeForce(1, 0, 1);
        }
        else if (Input.GetAxis("Vertical") < 0 && (Input.GetAxis("Horizontal") < 0))
        {
            GetComponent<Rigidbody>().AddRelativeForce(1, 0, 1);
        }
        */
        if (Input.GetAxis("Vertical") > 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(3*speed, 0, 0);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(3*-speed, 0, 0);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(0, 0, 3*-speed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(0, 0, 3*speed);
        }

        

        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            onGround = false;
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
