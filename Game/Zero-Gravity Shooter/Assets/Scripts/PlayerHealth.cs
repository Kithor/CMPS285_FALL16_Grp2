using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
	
	void Update ()
    {
        Debug.Log(health.ToString());

	    if (health <= 0)
        {
            Destroy(gameObject);
        }
	}
}
