using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] puzzleElements;
    public string puzzletag;
    public float Timer = 20.0f;
    private bool isTiming = false;
    private bool bActive = false;
    void Start()
    {
        puzzleElements = GameObject.FindGameObjectsWithTag(puzzletag);
        foreach (GameObject element in puzzleElements)
        {
            element.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isTiming == true)
        {
            Timer -= Time.deltaTime;
        }

        if(Timer <= 0.0f)
        {
            PuzzleEnd();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && bActive == false)
        {
            PuzzleBegin();
        }
    }

    private void PuzzleBegin()
    {
        foreach (GameObject element in puzzleElements)
        {
            element.SetActive(true);
            isTiming = true;
            bActive = true;
        }
    }

    private void PuzzleEnd()
    {
        foreach (GameObject element in puzzleElements)
        {
            if(element.GetComponent<GoalRing>() != null)
            {
                element.GetComponent<GoalRing>().Reset();
            }
            element.SetActive(false);
            isTiming = false;
            Timer = 20.0f;
            bActive = false;
        }
    }
}
