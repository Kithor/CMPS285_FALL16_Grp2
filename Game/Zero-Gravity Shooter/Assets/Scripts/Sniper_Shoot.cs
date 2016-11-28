using UnityEngine;
using System.Collections;

public class Sniper_Shoot : MonoBehaviour
{
    public float weaponDamage = 100;
    public float gunForce = 100;
    public float fireRate = 1f;
    public float weaponRange = 100;
    public float hitForce = 200f;
    public float spreadLimit = 1f;
    public Transform gunEnd;
    public Camera cam;

    private int shotsFired = 0;
    private int maxFired = 5;
    private float nextFire;
    private WaitForSeconds shotDuration = new WaitForSeconds(.02f);
    private LineRenderer laserLine;
    private Rigidbody player;

    void Start()
    {
        laserLine = gameObject.GetComponent<LineRenderer>();                                    //Gets the LineRenderer from the gun
        laserLine.material.color = Color.Lerp(Color.clear, Color.yellow, 0.5f);                 //Sets the Color and the length of the LineRenderer
        player = GetComponentInParent<Rigidbody>();                                             //Gets the Rigidbody from parent
    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 10, 10), "");                     //Add a reticle at the center of the screen
    }

    public void Fire()
    {
        if (shotsFired < maxFired && nextFire < Time.time)                                      //waits till game clock > nextFire
        {
            nextFire = Time.time + fireRate;                                                    //Sets the nextFire to the game clock + firerate

            StartCoroutine(ShotEffect());                                                       //Run method alongside Update

            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));           //Sets rayOrigin to the view from the camera
            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);                                          //laserLine(LineRenderer) origin set to gunEnd

            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);                                            //laserLine drawn to hitpoint, if hits object

                if (hit.collider.tag == "Player")
                {
                    var enemy = hit.collider.GetComponent<Rigidbody>();                         //Create variable for the enemy rigidbody
                    enemy.AddForce(transform.TransformDirection(new Vector3(0, 0, (gunForce))));    //Apply force to enemy player

                    var enemyHealth = hit.collider.GetComponent<PlayerHealth>();                //Create variable for the enemy health
                    enemyHealth.hitDelay = enemyHealth.hitTimer + Time.time;                    //Start the health regen clock for enemy player
                    enemyHealth.health -= weaponDamage;                                         //Reduce enemy health by weaponDamage
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (cam.transform.forward * weaponRange));    //laserLine drawn to weapon range, if it hits nothing
            }

            player.AddForce(transform.TransformDirection(new Vector3(0, 0, -(gunForce * 10))));        //Add force to the player when firing
            shotsFired++;                                                                       //Increment shotsFired
        }
    }

    public void Reload()
    {
        shotsFired = 0;
    }

    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;                                                               //Enable LineRenderer
        yield return shotDuration;                                                              //Wait untill the line has been drawn
        laserLine.enabled = false;                                                              //Disable LineRenderer
    }
}
