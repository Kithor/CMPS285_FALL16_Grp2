using UnityEngine;
using System.Collections;

public class WeaponFire : MonoBehaviour
{
    public Rigidbody projectile;
    public GameObject projectileOrigin;


    int shotsFired = 0;             //amount of shots fired
    int maxFired = 1;              //amount that can be fired

    private Rigidbody player;
    public float bulletSpeed = 25; //will be private, public for easy testing
  //public float fireRate = 0;      //private
    public float weaponRange = 50;  //private


    public float force = 50.0f;
    public float radius = 5.0f;
    public float upwardsModifier = 0.0f;
    public ForceMode forceMode;


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

            if (Input.GetMouseButtonDown(1))
            {
                foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
                {
                    if (col.GetComponent<Rigidbody>() != null)
                    {
                        col.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, upwardsModifier, forceMode);
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            shotsFired = 0;
        }
    }
}
