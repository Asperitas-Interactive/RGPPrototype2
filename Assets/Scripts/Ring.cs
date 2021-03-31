using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    private bool ringCheck = false;
    // Start is called before the first frame update
    private void OnEnable()
    {
        ringCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ringCheck = true;
        }
    }
}
