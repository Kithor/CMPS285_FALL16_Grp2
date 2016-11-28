using UnityEngine;
using System.Collections;

public class WeaponFire : MonoBehaviour
{
    public Rigidbody projectile;
    public GameObject projectileOrigin;


    int shotsFired = 0;             //amount of shots fired
    int maxFired = 33;              //amount that can be fired

    private Rigidbody player;
    public float bulletSpeed = 100; //will be private, public for easy testing
  //public float fireRate = 0;      //private
    public float weaponRange = 50;  //private

    void Start()
    {
        player = GetComponentInParent<Rigidbody>();
    }

	void Update ()
    {   //change value of firing when left click pressed

        if (Input.GetButton("Fire1") && shotsFired < maxFired)
        {
            Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, Quaternion.identity) as Rigidbody;   //Generate projectile
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, bulletSpeed));                     //Move Projectile                                                                                                                                  
            player.AddForce(-instantiatedProjectile.velocity);                                                                  //Move Parent Object (player)

            shotsFired++;

        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            shotsFired = 0;
        }
	}
}
