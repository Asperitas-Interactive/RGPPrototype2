using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    public CollectableCount UIImage;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIImage.onCollection();
            gameObject.SetActive(false);
        }
    }
}
