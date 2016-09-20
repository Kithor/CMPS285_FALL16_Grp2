using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float horizontalSpeed = 10.0f;
    public float verticalSpeed = 10.0f;

    private float yaw;
    private float pitch;

    void Update()
    {
        var rotX = transform.eulerAngles.x;

        if (rotX > -90 && rotX < -180)
        {
            yaw += horizontalSpeed * -Input.GetAxis("Mouse X");
            pitch -= verticalSpeed * Input.GetAxis("Mouse Y");
        }
        else if (rotX > 90 && rotX < 180)
        {
            yaw += horizontalSpeed * -Input.GetAxis("Mouse X");
            pitch -= verticalSpeed * Input.GetAxis("Mouse Y");
        }
        else
        {
            yaw += horizontalSpeed * Input.GetAxis("Mouse X");
            pitch -= verticalSpeed * Input.GetAxis("Mouse Y");
        }

        transform.eulerAngles = new Vector3(pitch, yaw, 0);
    }
}
