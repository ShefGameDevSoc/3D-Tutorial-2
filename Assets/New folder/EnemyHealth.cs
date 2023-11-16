using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth;
    public float currentHealth;
    public NavMeshAgent agent;
    public MeshRenderer MeshRenderer;
    EnemyStateManager stateManager;

    public float blinkIntensity;
    public float blinkDuration;
    float blinktimer;

    private float recoverTime = 30f;

    public bool death = false;
    private float DeathTimer = 60f;

    void Start()//getting all componets that will be needed in health system
    {
        agent = GetComponent<NavMeshAgent>();
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float amount)//reducing enemy health when hit
    {
        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            //checks if the enemy should die
            death = true;
            Die();
        }
        
        //updating healthbar
        blinktimer = blinkDuration;//shows the enemy being hit
        recoverTime = 30f;
    }

    private void Update()
    {
        blinktimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinktimer / blinkDuration);

        float Intensity = (lerp * blinkIntensity) + 1.0f;
        MeshRenderer.material.color = Color.white * Intensity;

        //check to see if the enemy can recover or not based on a recovery timer
        //current health and if the enemy is dead or not
        if (recoverTime > 0 && currentHealth < MaxHealth && death == false)
        {
            recoverTime-= Time.deltaTime;
        }
        else if(recoverTime == 0)
        {
            Recover();
        }

        //a timer for when the enemy despawns to improve performance
        if(death == true && DeathTimer > 0)
        {
            DeathTimer-= Time.deltaTime;
        }
        else if(DeathTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Die()
    {
        //deactiving componets so the enmey appears dead
        agent.SetDestination(transform.position);
        gameObject.GetComponent<EnemyStateManager>().enabled = false;

    }

    private void Recover()
    {
        while(currentHealth < MaxHealth)//loop for bring health for full use full for enemy's with different max healths
        {
            currentHealth = currentHealth + 1;
        }
        recoverTime = 30f;//resetting the recovery timer
    }
}