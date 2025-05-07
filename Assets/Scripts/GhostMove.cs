using UnityEngine;
using UnityEngine.AI;

public class GhostMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public float speed = 1;
    
    void Update()
    {
        Vector3 destination = Camera.main.transform.position;
        agent.SetDestination(destination);
        agent.speed = speed;
    }
}
