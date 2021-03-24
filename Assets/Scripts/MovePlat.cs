using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MovePlat : MonoBehaviour
{
    playerMovement pl;
    //The Distance the end points are
    public Vector3 DisplacementPos;
    public Vector3 DisplacementNeg;
    //The Speed you arrive at a end point
    public Vector3 VectorSpeed;
    //The Destination it checks
    private Vector3 DestinationMax;
    private Vector3 DestinationMin;
    // Start is called before the first frame update
    void Start()
    {
        DestinationMax = transform.position + DisplacementPos;
        DestinationMin = transform.position + DisplacementNeg;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
            pl = collision.gameObject.GetComponent<playerMovement>();

    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            pl.external = Vector3.zero;
            pl = null;
     
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Move towards location
        transform.Translate(VectorSpeed * Time.deltaTime, Space.World);
        //Clamp between for correct calculation
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, DestinationMin.x, DestinationMax.x),
            Mathf.Clamp(transform.position.y, DestinationMin.y, DestinationMax.y),
            Mathf.Clamp(transform.position.z, DestinationMin.z, DestinationMax.z));
        //check if reached
        DestinationReach();

        if(pl)
        {
            pl.external = VectorSpeed;
        }
    }

    //Check if it reached its destination
    void DestinationReach()
    {
        if(transform.position.x == DestinationMax.x && transform.position.y == DestinationMax.y && transform.position.z == DestinationMax.z)
        {
            VectorSpeed = -VectorSpeed;
        }

        if (transform.position.x == DestinationMin.x && transform.position.y == DestinationMin.y && transform.position.z == DestinationMin.z)
        {
            VectorSpeed = -VectorSpeed;
        }
    }

   
}
