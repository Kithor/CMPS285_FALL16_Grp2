using UnityEngine;
using System.Collections;

public class RaycastShoot : MonoBehaviour
{
    public float gunForce = 800;
    public float damage = 10;
    public float fireRate = 0.1f;
    public float weaponRange = 50;
    public float hitForce = 100f;
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
        laserLine = GetComponent<LineRenderer>();
        laserLine.material.color = Color.Lerp(Color.clear, Color.yellow, 0.5f);
        player = GetComponentInParent<Rigidbody>();
	}
	
	void Update()
    {
        if (Input.GetButton("Fire1") && shotsFired < maxFired && nextFire < Time.time)
        {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (cam.transform.forward * weaponRange));
            }

            player.AddForce(transform.TransformDirection(new Vector3(0, 0, -gunForce)));
            shotsFired++;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            shotsFired = 0;
        }
	}

    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;    
    }
}
