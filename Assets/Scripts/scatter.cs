using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class scatter : MonoBehaviour
{
        System.Random rnd = new System.Random();

    GameObject Animals;
    GameObject Waypoints;

    animal[] animals;

    // Start is called before the first frame update
    private void Awake()
    {
        Animals = transform.GetChild(0).gameObject;
        Waypoints = transform.GetChild(1).gameObject;
    }

    public struct animal
    {
        public GameObject an;
        public int way;
    }

    void Start()
    {
        animals = new animal[Animals.transform.childCount];

        scatterAway();
    }
    

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<animals.Length;i++)
        {
            if((animals[i].an.transform.position - Waypoints.transform.GetChild(animals[i].way).position).x <0.1f  && (animals[i].an.transform.position - Waypoints.transform.GetChild(animals[i].way).position).y <0.1f && (animals[i].an.transform.position - Waypoints.transform.GetChild(animals[i].way).position).z<0.1f)
            {
                //Animals.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Animals.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().velocity = (Waypoints.transform.GetChild(animals[i].way).transform.position - Animals.transform.GetChild(i).position).normalized * 2.0f;
                //Animals.transform.GetChild(i).right = Animals.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().velocity.normalized;
            }
        }
    }

    void scatterAway()
    {
        int num;

        for (int i =0;i<Animals.transform.childCount ;i++)
        {
            animals[i].an = Animals.transform.GetChild(i).gameObject;
            num = rnd.Next(0, Waypoints.transform.childCount);
            animals[i].way = num;
            Animals.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().velocity = (Waypoints.transform.GetChild(num).transform.position - Animals.transform.GetChild(i).position).normalized * 5.0f;
            Animals.transform.GetChild(i).forward = Animals.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().velocity.normalized;

        }
    }
}
