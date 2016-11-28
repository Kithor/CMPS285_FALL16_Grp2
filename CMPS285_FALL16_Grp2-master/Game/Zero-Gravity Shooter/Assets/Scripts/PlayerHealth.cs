using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour
{
    public const float maxhealth = 100;
    [SyncVar] public float health = maxhealth;                    //creates a "currenthealth" pool that is synced across the server for only localplayer

    public float healthRegen = 1;
    public float hitTimer = 10;
    public float hitDelay;

    private float regenTimer = 0.25f;
    private float regenDelay;
    private bool isRegening = false;

                           
	void Update ()
    {

        if(!isServer)                                               //syncs local player's health to server so that all players don't share same health amount
        {
            return;     
        }

        Debug.Log(health.ToString());

        if (health < 50 && !isRegening && hitDelay < Time.time)
        {
            StartCoroutine(HealthRegen());
        }

        if (health <= 0)
        {
            health = maxhealth;
            RpcRespawn();
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

    [ClientRpc]
    void RpcRespawn()
    {
       if (!isLocalPlayer)
        {
            transform.position = Vector3.zero;
        }
    }
}
