using UnityEngine;

public class Launcher_Shoot : MonoBehaviour
{
    PlayerController playerController;
    int shotsFired = 0;             //amount of shots fired
    int maxFired = 1;               //amount that can be fired

    public ForceMode forceMode;
    public Rigidbody projectile;
    public GameObject projectileOrigin;
    public float gunForce = 50.0f;
    public float radius = 5.0f;
    public float upwardsModifier = 0.0f;

    private Rigidbody player;
    public float bulletSpeed = 25;  //will be private, public for easy testing
    public float fireRate = 1;     //private
    public float nextFire = 0;
    public float weaponRange = 50;  //private

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        player = GetComponentInParent<Rigidbody>();
    }

	public void Fire()
    {   //change value of firing when left click pressed
        if (shotsFired < maxFired && nextFire < Time.time)
        {
            nextFire = Time.time + fireRate;

            Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, Quaternion.identity) as Rigidbody;   //Generate projectile
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, bulletSpeed));                     //Move Projectile            
            instantiatedProjectile.name = ("Rocket" + playerController.id);
                                                                                                                                  
            player.AddForce(transform.TransformDirection(new Vector3(0, 0, -(gunForce * 10))));                                                                                     //Move Parent Object (player)

            shotsFired++;
        }
    }

    public void Detonate()
    {
        var rocket = GameObject.Find("Rocket" + playerController.id).GetComponent<RocketExplode>();

        rocket.Detonate();
    }

    public void Reload()
    {
        shotsFired = 0;
    }
}
