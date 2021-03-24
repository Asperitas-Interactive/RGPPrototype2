using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderAI : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    public float radius;
    private Vector3 origin;
    [SerializeField]
    public float maxTimer;

    private float timer;

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0.0f)
        {
            Vector3 MovePos = RandomPositionInCircle(origin, 4);
            agent.SetDestination(MovePos);
            timer = maxTimer;
        }
    }

    Vector3 RandomPositionInCircle(Vector3 origin, float radius) 
    {
        Vector3 RandPos = Random.insideUnitSphere * radius;

        RandPos += origin;

        NavMeshHit hit;

        NavMesh.SamplePosition(RandPos, out hit, radius, NavMesh.AllAreas);

        return hit.position;
    }
}
