using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightShooting : MonoBehaviour
{
	public Camera cam;
	public GameObject projectile;
	public Transform torchtip;
	public float projectileSpeed = 30f;
    public float fireRate =4;

    public float damage = 10f;

    private Vector3 destinantion;
	private bool place;
    private float timetofire;
	
    void Start()
    {
        GameObject.Find("Bullet").GetComponent<projectile>().Pdamage = damage;
        //tells the projetile script whe damage to tell the enemy to take when hit
    }
    void Update()
    {
        if(Input.GetMouseButton(1) && Time.time >= timetofire)//input of mouse button and a fire rate
		{
            timetofire = Time.time +1/fireRate ;
			shootProjectile();
		}
    }
	void shootProjectile()
	{
		Ray ray = cam.ViewportPointToRay(new Vector3(0.5f , 0.5f , 0));//shots ray to find where to shoot projetile to
		RaycastHit hit;
		
		if(Physics.Raycast(ray, out hit))
			destinantion = hit.point;
		else
            //if no point could be determind in 1000 meters then the point is set to 1000 meters from the player
            destinantion = ray.GetPoint(1000);
		
		InstantiateProjectile(torchtip);
	}
	
	void InstantiateProjectile(Transform firePoint)//spawns the projetile from the torchs tip
	{
		var projectileObj = Instantiate (projectile, firePoint.position, firePoint.rotation)as GameObject;
		projectileObj.GetComponent<Rigidbody>().velocity = (destinantion - firePoint.position).normalized * projectileSpeed;
	}
	
}
