using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    private Vector3 PlayerDestinantion;
    public float projectileSpeed = 30f;
    public GameObject Enemyprojectile;
    public float shootTimer;
    public ChaseState chaseState;
    

    public override EnemyState RunCurrentState(EnemyStateManager Enemy)
    {
        Enemy.FacePlayer();

        float distance = Vector3.Distance(Enemy.playersTransform.position, Enemy.enemypos.position);

        
        if (distance > Enemy.AttackRange)//if out of range switch to chasestate
        {
            shootTimer = 2;
            return chaseState;
        }
        if (shootTimer <= 0.5)//fire rate 
        {
            RaycastHit Hit;
            Ray ray = new Ray(Enemy.sensor.transform.position, transform.forward);

            if (Physics.Raycast(ray, out Hit))
            {

                PlayerDestinantion = Hit.point;
                Debug.DrawLine(ray.origin, Hit.point);
                InstantiateEnemyProjectile(Enemy.Eye1);
                shootTimer = 2;

    
            }
        }
        if (shootTimer > 0)//decresing the fire rate
        {
            shootTimer -= Time.deltaTime;
            shootTimer = Mathf.Max(shootTimer, 0);

        }
        return this;

    }
    void InstantiateEnemyProjectile(Transform firePoint)//similar to player shooting
    {
        var projectileObj = Instantiate(Enemyprojectile, firePoint.position, firePoint.rotation) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (PlayerDestinantion - firePoint.position).normalized * projectileSpeed;

    }

}

