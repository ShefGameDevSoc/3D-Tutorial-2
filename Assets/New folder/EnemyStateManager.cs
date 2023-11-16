using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    public EnemyState currentState; 
    
    public NavMeshAgent agent;
    [SerializeField] public Transform playersTransform;
    public Transform enemypos;

    public Transform sensor,Eye1;

    public float turnRate;
    public float AttackRange = 5f;
    public float soundrange = 6f;

    void Update()
    {   
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        if (currentState != null)//runs referenced current state to run
        {
            EnemyState nextState = currentState.RunCurrentState(this);

            if (nextState != null)//checks if the state needs to be switchs
            {
                SwitchState(nextState);
            }
        }
    }
    private void SwitchState(EnemyState nextState)//changes state
    {
        currentState = nextState;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.DrawWireSphere(transform.position, soundrange);
    }
    public void FacePlayer()
    {
        Vector3 targetDelta = playersTransform.transform.position - enemypos.transform.position;//the vector from the player to the enemy
        targetDelta.y = 0;//0 the y so the enemy does not bug out and lean to the exact y postion of the player
        float angleToTarget = Vector3.Angle(enemypos.transform.forward, targetDelta);//calculating the angle
        Vector3 turnAxis = Vector3.Cross(enemypos.transform.forward, targetDelta);//vector of turn towards the player 

        //rotating the enemy toward the player based of prevoius calculations
        enemypos.transform.RotateAround(enemypos.transform.position, turnAxis, Time.deltaTime * turnRate * angleToTarget);
    }
}
