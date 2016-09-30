using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Rigidbody player;
    public float horizontalSpeed = 10.0f;
    public float verticalSpeed = 10.0f;
    public float rotateSpeed = 70.0f;

    private float yaw;
    private float pitch;
    private float rotation;

    void Update()
    {
        yaw = horizontalSpeed * Input.GetAxis("Mouse X");
        pitch = verticalSpeed * Input.GetAxis("Mouse Y");
        rotation = rotateSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, yaw);
        transform.Rotate(Vector3.left, pitch);

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.forward, -rotation);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward, rotation);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        player.velocity = Vector3.zero;
    }
}
