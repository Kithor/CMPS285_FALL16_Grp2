using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float horizontalSpeed = 30.0f;
    public float verticalSpeed = 30.0f;
    //public float rotationSpeed = 100.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    //private float rotate = 0.0f;

    void Update()
    {
        yaw += horizontalSpeed * Input.GetAxis("Mouse X");
        pitch -= verticalSpeed * Input.GetAxis("Mouse Y");
        //rotate += rotationSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        //if (Input.GetKey(KeyCode.Q))                              //not working quite right, putting a pin in it
        //{
        //    transform.eulerAngles = new Vector3(0, 0, rotate);
        //}

        //if (Input.GetKey(KeyCode.E))
        //{
        //    transform.eulerAngles = new Vector3(0, 0, -rotate);
        //}

    }
}
