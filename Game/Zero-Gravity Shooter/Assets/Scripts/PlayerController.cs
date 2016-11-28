using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    NetworkManager netManager;
    ScoreManager scoreManager;
    GameObject scoreboard;
    GameObject weapon;
    int weaponNum = 1;

    [SyncVar] public int id = 0;
    [SyncVar] public int kills = 0;
    [SyncVar] public int deaths = 0;
    public Camera cam;
    public float horizontalSpeed = 10.0f;
    public float verticalSpeed = 10.0f;
    public float rotateSpeed = 70.0f;

    private Rigidbody player;
    private int killsLast;
    private int deathsLast;
    private float yaw;
    private float pitch;
    private float rotation;

    void Start()
    {
        netManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        id = netManager.numPlayers;

        scoreboard = GameObject.Find("Scoreboard");
        scoreboard.SetActive(false);

        scoreManager = scoreboard.GetComponent<ScoreManager>();
        scoreManager.AddScore(id, kills, deaths);

        player = gameObject.GetComponent<Rigidbody>();
        weapon = transform.FindChild("Assault Rifle").gameObject;
        killsLast = kills;
        deathsLast = deaths;
    }

    void Update()
    {
        if (!isLocalPlayer)                                                     //Only local user can control the local player 
        {
            cam.enabled = false;
            return;
        }

        yaw = horizontalSpeed * Input.GetAxis("Mouse X");                       //Yaw equals the speed multiplied by the mouse X-axis
        pitch = verticalSpeed * Input.GetAxis("Mouse Y");                       //Pitch equals the speed multiplied by the mouse Y-axis
        rotation = rotateSpeed * Time.deltaTime;                                //Rotation equals the rotation speed multiplied by deltaTime
                                                    
        transform.Rotate(Vector3.up, yaw);                                      //Rotate around the Y-axis with the X-input
        transform.Rotate(Vector3.left, pitch);                                  //Rotate around the X-axis with the Y-input

        //Rotate player on key press
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.forward, -rotation);                       //When E is pressed -rotate around Z-axis
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward, rotation);                        //When Q is pressed rotate around Z-axis
        }
        
        //Fire weapon on key press
        if (weaponNum == 1) //Assault Rifle
        {
            if (Input.GetButton("Fire1"))
                weapon.GetComponent<Rifle_Shoot>().Fire();

            if (Input.GetKeyDown(KeyCode.R))
                weapon.GetComponent<Rifle_Shoot>().Reload();
        }
        
        if (weaponNum == 2) //Sniper Rifle
        {
            if (Input.GetButtonDown("Fire1"))
                weapon.GetComponent<Sniper_Shoot>().Fire();

            if (Input.GetKeyDown(KeyCode.R))
                weapon.GetComponent<Sniper_Shoot>().Reload();
        }

        if (weaponNum == 3) //Rocket Launcher
        {
            if (Input.GetButtonDown("Fire1"))
                weapon.GetComponent<Launcher_Shoot>().Fire();

            if (Input.GetButtonDown("Fire2"))
                weapon.GetComponent<Launcher_Shoot>().Detonate();

            if (Input.GetKeyDown(KeyCode.R))
                weapon.GetComponent<Launcher_Shoot>().Reload();
        }

        //Show Scoreboard
        if (scoreboard == null)
            scoreboard = GameObject.Find("Scoreboard");

        else
        {
            if (Input.GetKey(KeyCode.Tab))
                scoreboard.SetActive(true);
            else
                scoreboard.SetActive(false);
        }

        //Update when score changes
        if(kills != killsLast)
        {
            scoreManager.UpdateScore(id, "Kills", kills);                       //When #Kills changes update score
            killsLast = kills;
        }
        if(deaths != deathsLast)
        {
            scoreManager.UpdateScore(id, "Deaths", deaths);                     //When #Deaths changes update score
            deathsLast = deaths;
        }

        //Switch weapons
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            weapon.SetActive(false);
            weapon = transform.FindChild("Assault Rifle").gameObject;
            weaponNum = 1;
            weapon.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            weapon.SetActive(false);
            weapon = transform.FindChild("Sniper Rifle").gameObject;
            weaponNum = 2;
            weapon.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            weapon.SetActive(false);
            weapon = transform.FindChild("Rocket Launcher").gameObject;
            weaponNum = 3;
            weapon.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision col)                                        //Runs on collision
    {
        var health = GetComponentInParent<PlayerHealth>();                      //Creates a variable for the Parent Objects health
        player.angularVelocity = Vector3.zero;                                  //On collision stop player rigidbody rotation
        player.velocity = -(player.velocity)/2;                                 //On collision change direction

        if (col.relativeVelocity.magnitude > 3)
        {   
            health.health -= col.relativeVelocity.magnitude;                    //Take damage on impact with an object
        }
    }

    void OnTriggerEnter()
    {
        var health = GetComponentInParent<PlayerHealth>();
        player.velocity = Vector3.zero;
        health.health = -10;
    }
}
