using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerController : NetworkBehaviour
{  
    public Camera cam;
    public float horizontalSpeed = 10.0f;
    public float verticalSpeed = 10.0f;
    public float rotateSpeed = 70.0f;

    private Rigidbody player;
    private float yaw;
    private float pitch;
    private float rotation;

    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(!isLocalPlayer)                                                      //If statement so that only local player can control the local player, and not all entities 
        {
            cam.enabled = false;
            return;
        }

        yaw = horizontalSpeed * Input.GetAxis("Mouse X");                       //Yaw equals the speed multiplied by the mouse X-axis
        pitch = verticalSpeed * Input.GetAxis("Mouse Y");                       //Pitch equals the speed multiplied by the mouse Y-axis
        rotation = rotateSpeed * Time.deltaTime;                                //Rotation equals the rotation speed multiplied by deltaTime
                                                    
        transform.Rotate(Vector3.up, yaw);                                      //Rotate around the Y-axis with the X-input
        transform.Rotate(Vector3.left, pitch);                                  //Rotate around the X-axis with the Y-input

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.forward, -rotation);                       //When E is pressed -rotate around Z-axis
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward, rotation);                        //When Q is pressed rotate around Z-axis
        }

        /*if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");                                 //When escape is pressed, return to main menu
        }*/

    }

    void OnCollisionEnter(Collision col)                                        //Runs on collision
    {
        var health = GetComponentInParent<PlayerHealth>();                      //Creates a variable for the Parent Objects health
        player.angularVelocity = Vector3.zero;                                  //On collision stop player rigidbody rotation
        player.velocity = -player.velocity / 2;                                 //On collision change direction

        if (col.relativeVelocity.magnitude > 3)
        {   
            health.health -= col.relativeVelocity.magnitude;                    //Take damage on impact with an object
        }
    }
}
