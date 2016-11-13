using UnityEngine;
using System.Collections;

public class Shotgun_Shoot : MonoBehaviour
{
    public float weaponDamage = 10;
    public float gunForce = 20;
    public float fireRate = 0.1f;
    public float weaponRange = 50;
    public float hitForce = 100f;
    public float spreadLimit = 1f;
    public Transform gunEnd;
    public Camera cam;

    private int shotsFired = 0;
    private int maxFired = 32;
    private float nextFire;
    private WaitForSeconds shotDuration = new WaitForSeconds(.02f);
    private LineRenderer laserLine;
    private Rigidbody player;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();                                               //Gets the LineRenderer from the gun
        laserLine.material.color = Color.Lerp(Color.clear, Color.yellow, 0.5f);                 //Sets the Color and the length of the LineRenderer
        player = GetComponentInParent<Rigidbody>();                                             //Gets the Rigidbody from parent
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && shotsFired < maxFired && nextFire < Time.time)          //waits till game clock > nextFire
        {
            nextFire = Time.time + fireRate;                                                    //Sets the nextFire to the game clock + firerate
            shotsFired++;
            StartCoroutine(ShotEffect());                                                       //Run method alongside Update
            StartCoroutine(Fire());
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            shotsFired = 0;                                                                     //When R pressed reload
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 10, 10), "");                     //Add a reticle at the center of the screen
    }

    IEnumerator Fire()
    {
        for (int i = 0; i < 10; i++)
        {
            var spread = transform.up * Random.Range(0, spreadLimit);
            spread = (Quaternion.AngleAxis(Random.Range(0, 360), transform.forward) * spread);
            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));               //Sets rayOrigin to the view from the camera

            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);                                              //laserLine(LineRenderer) origin set to gunEnd

            if (Physics.Raycast(rayOrigin, spread, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);                                                //laserLine drawn to hitpoint, if hits object

                if (hit.collider.tag == "Player")
                {
                    var enemy = hit.collider.GetComponent<Rigidbody>();                             //Create variable for the enemy rigidbody
                    enemy.AddForce(transform.TransformDirection(new Vector3(0, 0, (gunForce))));    //Apply force to enemy player

                    var enemyHealth = hit.collider.GetComponent<PlayerHealth>();                    //Create variable for the enemy health
                    enemyHealth.hitDelay = enemyHealth.hitTimer + Time.time;                        //Start the health regen clock for enemy player
                    enemyHealth.health -= weaponDamage;                                             //Reduce enemy health by weaponDamage
                }
            }
            else
            {
                laserLine.SetPosition(1, spread + (cam.transform.forward * weaponRange));           //laserLine drawn to weapon range, if it hits nothing
            }

            player.AddForce(transform.TransformDirection(new Vector3(0, 0, -gunForce)));            //Add force to the player when firing
            yield return hit;
        }
    }

    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;                                                               //Enable LineRenderer
        yield return shotDuration;                                                              //Wait untill the line has been drawn
        laserLine.enabled = false;                                                              //Disable LineRenderer
    }
}