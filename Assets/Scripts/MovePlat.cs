using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlat : MonoBehaviour
{
    public Vector3 Destination;
    public Vector3 VectorSpeed;
    public Vector3 Origin;
    // Start is called before the first frame update
    void Start()
    {
        Origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(VectorSpeed * Time.deltaTime, Space.World);
        DestinationReach();
    }

    void DestinationReach()
    {
        if(transform.position.x == Destination.x && transform.position.y == Destination.y && transform.position.z == Destination.z)
        {
            VectorSpeed = -VectorSpeed;
        }
    }
}
