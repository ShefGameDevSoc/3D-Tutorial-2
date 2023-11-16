using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : EnemyState
{
    public IdleState idleState;
    public AttackState AttackState;
    public EnemySenesor Range;

    private float timer = 0.0f;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    

    public override EnemyState RunCurrentState(EnemyStateManager Enemy)
    {
        Range.angle = 360;//incresing the sensor angle to make the enemey more aware of the player in chase state

        //the Distance between player and enemy
        float Distancefromplayer = Vector3.Distance(Enemy.playersTransform.position, Enemy.enemypos.position);
        timer -= Time.deltaTime;

        if (timer < 0.0f)
        {
            float sqDistance = (Enemy.playersTransform.position - Enemy.agent.destination).sqrMagnitude;
            if (sqDistance > maxDistance * maxDistance)
            {
                Enemy.agent.destination = Enemy.playersTransform.position;
                
            }
            timer = maxTime;

        }
        if(Distancefromplayer <= Enemy.agent.stoppingDistance)//if the enemy is in stopping distance faceplayer
        {
            Enemy.FacePlayer();
        }
        if(Distancefromplayer < Enemy.AttackRange){//when in attack range switch to attack state

            return AttackState;
        }
        if(Range.PlayerSeen == false)//if out of chase range switch back to idle
        {
            return idleState;
        }
        
        return this;
    }

}
