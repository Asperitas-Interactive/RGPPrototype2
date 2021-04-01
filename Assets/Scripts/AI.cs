using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public bool Wander;
    public bool scatter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Wander && !scatter)
        {
            scatter = true;
            for (int i = 0; i < transform.GetChild(0).gameObject.transform.childCount; i++)
            {
                transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.GetComponent<WanderAI>().enabled = true; 
            }
            GetComponent<scatter>().enabled = false;
        }

        else 
        {
            GetComponent<scatter>().enabled = true;
            for (int i = 0; i < transform.GetChild(0).gameObject.transform.childCount; i++)
            {
                //transform.GetChild(0).gameObject.transform.GetChild(i).gameObject.GetComponent<WanderAI>().enabled = false;
            }
        }
    }
}
