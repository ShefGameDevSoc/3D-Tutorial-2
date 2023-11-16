using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
    public ChaseState chaseState;
    public PatrolState PatrolState;
    public EnemySenesor sense;
    public float timeRemaining = 3f;
    public bool patrol = false;

    public override EnemyState RunCurrentState(EnemyStateManager Enemy)
    {

        sense.angle = 125;

        float Distancefromplayer = Vector3.Distance(Enemy.playersTransform.position, Enemy.enemypos.position);

        if (Enemy.soundrange >= Distancefromplayer)
        {

            return chaseState;
        }
        if(sense.PlayerSeen)
        {
            
            return chaseState;

        }
        else 
        {
            if (timeRemaining > 0 && patrol == true)//a timer till idle state switches to patrol state
            {
                timeRemaining -= Time.deltaTime;
                return this;
            }
            else
            {
                timeRemaining = 10f;//reset the timer if enemy switches back to idle
                return PatrolState;
            }
            
        }
        
    }

}
