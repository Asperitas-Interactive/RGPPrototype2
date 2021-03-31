using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalRing : MonoBehaviour
{
    public Collectable collectable;
    public Ring[] Rings;
    // Start is called before the first frame update
    void Start()
    {
        collectable.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            int rCount = 0;
            foreach(Ring r in Rings)
            {
                if(r.ringCheck == true)
                {
                    rCount++;
                }
            }

            if(rCount == Rings.Length)
            {
                collectable.gameObject.SetActive(true);
            }
        }
    }
}
