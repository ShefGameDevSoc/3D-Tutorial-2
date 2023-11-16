using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyprojectile : MonoBehaviour
{
    public float Pdamage = 10f;
    private bool collided;
    private float despawnTimer = 15f;

    void Update()
    {
        //despawning the projectile if not hit anything in timer
        if(despawnTimer > 0)
        {
            despawnTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
            despawnTimer = 5f;
        }
    }

    private void OnCollisionEnter(Collision co)// when the bullet hits something
    {

        if (co.gameObject.tag != "Enemy" && co.gameObject.tag != "Bullet" && co.gameObject.tag == "player" && !collided)//if the projectils hits an enemy
        {
            collided = true;
            Destroy(gameObject);//make sure the projetile dissapears when collided
            co.gameObject.GetComponentInParent<PlayerHealth>().Damaged(Pdamage);
            
        }
        else if (co.gameObject.tag != "Bullet" && co.gameObject.tag != "Player" && !collided)
        {
            collided = true;
            Destroy(gameObject);
        }

    }

}
