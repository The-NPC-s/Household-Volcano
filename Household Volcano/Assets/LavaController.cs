using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    public bool lava_enabled = true;
    public float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lava_enabled)
        {
            Rise();
        }
    }

    private void Rise()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
    }
}
