using UnityEngine;
using System.Collections;

public class RocketExplode : MonoBehaviour
{
    public float force = 50.0f;
    public float radius = 5.0f;
    public float upwardsModifier = 0.0f;
    public float weaponDamage = 40;
    public ForceMode forceMode;




    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
            {   
                if (col.tag == "Player")
                {
                    var enemy = col.GetComponent<Rigidbody>();                             //Create variable for the enemy rigidbody
                    enemy.AddForce(transform.TransformDirection(new Vector3(0, 0, (force))));    //Apply force to enemy player

                    var enemyHealth = col.GetComponent<PlayerHealth>();                    //Create variable for the enemy health
                    enemyHealth.hitDelay = enemyHealth.hitTimer + Time.time;                        //Start the health regen clock for enemy player
                    enemyHealth.health -= weaponDamage;                                             //Reduce enemy health by weaponDamage
                }
                if (col.GetComponent<Rigidbody>() != null)
                {
                    col.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, upwardsModifier, forceMode);
                }
            }

            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
        {
            if (col.GetComponent<Rigidbody>() != null)
            {
                col.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, upwardsModifier, forceMode);
            }
        }

        Destroy(gameObject);
    }
}
