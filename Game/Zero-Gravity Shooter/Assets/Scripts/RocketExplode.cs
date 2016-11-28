using UnityEngine;

public class RocketExplode : MonoBehaviour
{
    public ForceMode forceMode;
    public float force = 50.0f;
    public float radius = 10.0f;
    public float upwardsModifier = 0.0f;
    public float weaponDamage = 40;

    public void Detonate()
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
        {   
            if (col.tag == "Player")
            {
                var enemy = col.GetComponent<Rigidbody>();                                      //Create variable for the enemy rigidbody
                enemy.AddForce(transform.TransformDirection(new Vector3(0, 0, (force))));       //Apply force to enemy player

                var enemyHealth = col.GetComponent<PlayerHealth>();                             //Create variable for the enemy health
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

    void OnCollisionEnter(Collision collision)
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
        {
            if (col.tag == "Player")
            {
                var enemy = col.GetComponent<Rigidbody>();
                enemy.AddForce(transform.TransformDirection(new Vector3(0, 0, (force))));

                var enemyHealth = col.GetComponent<PlayerHealth>();
                enemyHealth.hitDelay = enemyHealth.hitTimer + Time.time;
                enemyHealth.health -= weaponDamage;
            }

            if (col.GetComponent<Rigidbody>() != null)
            {
                col.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, upwardsModifier, forceMode);
            }
        }
        Destroy(gameObject);
    }
}
