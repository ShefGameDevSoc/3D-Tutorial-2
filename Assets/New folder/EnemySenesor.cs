using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenesor : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject PlayerRef;

    public LayerMask targetmask;
    public LayerMask ObstuctionMask;

    public bool PlayerSeen;

    void Start()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        //the field of veiw only checks every 0.2 seconds saving performance
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetmask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)//with in a cone shape for more acurrate FOV
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, ObstuctionMask))//checks if an obsticule is between enemy and Player
                {
                    PlayerSeen = true;
                }
                else
                    PlayerSeen = false;

            }
            else
                PlayerSeen = false;

        }
        else if (PlayerSeen)
            PlayerSeen = false;
    }

}
