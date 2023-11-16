using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiLocomotion : MonoBehaviour
{
    [SerializeField] public Transform playersTransform;

    NavMeshAgent agent;

    float timer = 0.0f;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    public void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0.0f)//timer is implementend so it only updates the target destination every second
        {
            float sqDistance = (playersTransform.position - agent.destination).sqrMagnitude;//distance between enemy and player
            if (sqDistance > maxDistance * maxDistance)
            {
                agent.destination = playersTransform.position;//set the destination for the player
            }
            timer = maxTime;//resets the timer
        }

    }
}

