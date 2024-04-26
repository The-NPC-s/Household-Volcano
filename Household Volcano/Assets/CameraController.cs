using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Obstruction;
    GameObject Player;
    private float DistanceToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        DistanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
    }

    void LateUpdate()
    {
        ViewObstructed();
    }

    void ViewObstructed()
    {

        if (Physics.Raycast(transform.position, Player.transform.position - transform.position, out RaycastHit hit, DistanceToPlayer + 0.1f))
        {
            if (!hit.collider.gameObject.CompareTag("Player"))
            {
                Obstruction = hit.transform;
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

            }
            else
            {
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }
    }
}
