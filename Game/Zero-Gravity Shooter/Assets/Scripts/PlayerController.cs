using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Rigidbody player;
    public float horizontalSpeed = 10.0f;
    public float verticalSpeed = 10.0f;

    private float yaw;
    private float pitch;

    void Update()
    {
        yaw = horizontalSpeed * Input.GetAxis("Mouse X");
        pitch = verticalSpeed * Input.GetAxis("Mouse Y");

        //transform.localEulerAngles = new Vector3(pitch, yaw, 0);
        transform.Rotate(Vector3.up, yaw);
        transform.Rotate(Vector3.left, pitch);
    }

    void OnCollisionEnter(Collision col){}
}
