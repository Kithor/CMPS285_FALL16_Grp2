using UnityEngine;
using System.Collections;

public class WeaponFire : MonoBehaviour
{
    public Rigidbody projectile;
    private float fireRate = 100;
	
	void Update ()
    {   //Run when button pressed
	    if (Input.GetButtonDown("Fire1")) 
        {   //Generate projectile
            Rigidbody generateProjectile = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;

            //Move Projectile
            generateProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, fireRate));
        }
	}
}
