using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public int sensitivity = 1;
    public int jumpForce = 10;
    public bool onGround = true;


    // Start is called before the first frame update
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
    }

    private void Move()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(10, 0, 0);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(-10, 0, 0);
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(0, 0, -10);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(0, 0, 10);
        }

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
        }
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * Input.GetAxis("Mouse X"));
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("trigger enter " + other.transform.name);
        if (!onGround && other.transform.tag == "Ground")
        {
            onGround = true;
        }
    }   
}
