using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float horizontalSpeed = 10.0f;
    public float verticalSpeed = 10.0f;
    //public float rotationSpeed = 100.0f;

    private float yaw;
    private float pitch;
    //private float rotate;

    void Update()
    {
        yaw += horizontalSpeed * Input.GetAxis("Mouse X");
        pitch -= verticalSpeed * Input.GetAxis("Mouse Y");
        //rotate = rotationSpeed * Time.deltaTime;

        //if (Input.GetKey(KeyCode.Q))                              //not working quite right, putting a pin in it
        //{
        //    transform.Rotate(0, 0, rotate);
        //}

        //if (Input.GetKey(KeyCode.E))
        //{
        //    transform.Rotate(0, 0, -rotate);
        //}

        transform.eulerAngles = new Vector3(pitch, yaw, 0);
    }
}
