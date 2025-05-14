using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public float speed = 1;
    public Animator animator;
    private float eatDistance = 0.3f;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Vector3 destination = Camera.main.transform.position;
        // agent.SetDestination(destination);
        // agent.speed = speed;

        if (!agent.enabled) return;

        GameObject closest = GetClosestOrb();
        if (closest == null) return;
        
        Vector3 targetPosition = closest.transform.position;
        agent.SetDestination(targetPosition);
        agent.speed = speed;
    }

    public void Kill()
    {
        agent.enabled = false;
        animator.SetTrigger("Death");
    }

    public void Destroy()
    {
        Destroy(gameObject);
        gameManager.ghostsNumber--;
        gameManager.UpdateUI();
    }

    public GameObject GetClosestOrb()
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        List<GameObject> orbs = OrbsSpawner.Instance.spawnedOrbs;
        foreach (var item in orbs)
        {
            Vector3 ghostPosition = transform.position;
            ghostPosition.y = 0;
            Vector3 orbPosition = item.transform.position;
            orbPosition.y = 0;
            float d = Vector3.Distance(ghostPosition, orbPosition);
            if (d < minDistance)
            {
                minDistance = d;
                closest = item;
            }
        }

        if (minDistance < eatDistance)
        {
            OrbsSpawner.Instance.DestroyOrb(closest);
        }
        
        return closest;
    }
}
