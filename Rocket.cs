using UnityEngine;
using System.Collections;

public class RocketExplode2 : MonoBehaviour
{
    public float force = 50.0f;
    public float radius = 5.0f;
    public float upwardsModifier = 0.0f;
    public ForceMode forceMode;




    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            foreach (Collider col in Physics.OverlapSphere(transform.position, radius))
            {
                if (col.GetComponent<Rigidbody>() != null)
                {
                    col.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius, upwardsModifier, forceMode);
                }
            }
        }
    }
}
