using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BirdFlee : MonoBehaviour
{
    public GameObject bird;

    bool isFleeing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isFleeing = true;
            bird.GetComponent<WanderAI>().enabled = false;
            bird.GetComponent<NavMeshAgent>().enabled = false;
            bird.GetComponent<Rigidbody>().AddForce(0.0f, 10.0f, 0.0f, ForceMode.Impulse);
            bird.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isFleeing = false;
            bird.GetComponent<WanderAI>().enabled = true;
            bird.GetComponent<NavMeshAgent>().enabled = true;
            bird.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
