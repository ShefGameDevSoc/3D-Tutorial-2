using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyState
{
    public ChaseState chaseState;
    public IdleState idlestate;
    public EnemySenesor sense;

    public LayerMask Ground;
    public LayerMask Obstruction;

    public EnemyStateManager Enemy;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    RaycastHit hit;

    public override EnemyState RunCurrentState(EnemyStateManager Enemy)
    {
        sense.angle = 125;
        Patroling();

        //the Distance between player and enemy
        float Distancefromplayer = Vector3.Distance(Enemy.playersTransform.position, Enemy.enemypos.position);

        if(Distancefromplayer < Enemy.soundrange)//if player in sound range then change to chase state
        {
            return chaseState;
        }
        if (sense.PlayerSeen)//if the player is seen by the sensor script change to chase state
        {
            return chaseState;

        }
        else
        {
            return this;
        }

    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)//enemy go to walkpoint
        {
            Enemy.agent.SetDestination(walkPoint);
        }

        Vector3 distanceTowalkPoint = transform.position - walkPoint;

        if (distanceTowalkPoint.magnitude < 2f)//for finding a new walkpoint when at stopping distance from the current
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //finding a random point within a range
        float randomz = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        //holding the walkpoint based of the random values added to the currentpostion
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomz);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))//check if the point is on walkable ground
        {
            if (Physics.SphereCast(walkPoint, 10f, transform.up, out hit , 5f , Obstruction))//checks to see if the walkpoint is obstucuted
            {
                if (Physics.SphereCast(walkPoint, 10f, transform.forward, out hit, 5f, Obstruction))
                {
                    walkPointSet = false;
                }
                
            }
            else
            {
                walkPointSet = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);

    }
}
