using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public bool enabled = true;
    public float speed = 0.01f;

    public float riseSpeed = 0.1f; // Speed of lava rising
    public float maxHeight = 10f; // Maximum height for lava to reach
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if lava has not reached its max height
        if (transform.position.y < maxHeight)
        {
            // Move lava up based on the riseSpeed
            transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);
        }
        /*
        if (enabled)
        {
            Rise();
        }
        */
    }

    private void Rise()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
    }
}
