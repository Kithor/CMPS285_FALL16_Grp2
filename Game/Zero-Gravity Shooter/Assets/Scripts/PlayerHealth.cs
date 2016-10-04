using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public int healthRegen = 2;
    public float hitTimer = 10;
    public float hitDelay;

    private float regenTimer = 0.5f;
    private float regenDelay;
    private bool isRegening = false;

	void Update ()
    {
        Debug.Log(health.ToString());

        if (health < 50 && !isRegening && hitDelay < Time.time)
        {
            StartCoroutine(HealthRegen());
        }

	    if (health <= 0)
        {
            Destroy(gameObject);
        }
	}

    private IEnumerator HealthRegen()
    {
        isRegening = true;
        while (health < 50 && regenDelay < Time.time)
        {
            regenDelay = regenTimer + Time.time;

            health += healthRegen;
            yield return new WaitForSeconds(regenTimer);
        }
        isRegening = false;
    }
}
