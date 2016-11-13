using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class PlayerHealth : NetworkBehaviour
{
    public const float maxhealth = 100;
    [SyncVar] public float health = maxhealth;                    //creates a "currenthealth" pool that is synced across the server for only localplayer

    public float healthRegen = 1;
    public float hitTimer = 10;
    public float hitDelay;
    public Slider healthSlider;

    private float regenTimer = 0.25f;
    private float regenDelay;
    private bool isRegening = false;
    private NetworkStartPosition[] spawnPoints;

    void Start()
    {
        //healthSlider = GameObject.Find("HUDCanvas").GetComponent<Slider>();
    }

    void Update()
    {
        if(!isServer)                                               //syncs local player's health to server so that all players don't share same health amount
        {
            return;     
        }

        if (health < 50 && !isRegening && hitDelay < Time.time)
        {
            StartCoroutine(HealthRegen());
        }

        if (health <= 0)
        {
            health = maxhealth;
            RpcRespawn();
        }

        healthSlider.value = health;
        spawnPoints = FindObjectsOfType<NetworkStartPosition>();
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
       if (isLocalPlayer)
        {
            Vector3 spawnPoint = Vector3.zero;                                                                  //Set the spawn point to origin as a default value

            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }

            transform.position = spawnPoint;                                                                    //Set the player’s position to the chosen spawn point
        }
    }
}
