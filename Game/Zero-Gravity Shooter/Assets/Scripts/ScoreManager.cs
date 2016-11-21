using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    int idTemp;
    int killsTemp;
    int deathsTemp;
    int num_players;
    GameObject player;
    Dictionary<int, Dictionary<string, int>> scoreboard;

    public GameObject playerEntryPrefab;

    void Start()
    {
        num_players = Network.connections.Length;
        scoreboard = new Dictionary<int, Dictionary<string, int>>();
        RefreshScoreBoard();
    }

    void Update()
    {
        if(num_players != Network.connections.Length)
        {
            RefreshScoreBoard();
        }
    }

    void RefreshScoreBoard()
    {
        num_players = Network.connections.Length;
        for(int i = 0; i < num_players; i++)
        {
            if (i == 0)
            {
                player = GameObject.Find("Player(Clone)");
            }
            else
            {
                player = GameObject.Find("Player(Clone)" + i);
            }
            idTemp = player.GetComponent<PlayerController>().id;
            killsTemp = player.GetComponent<PlayerController>().kills;
            deathsTemp = player.GetComponent<PlayerController>().deaths;
            SetScore(i, "Player ID", idTemp);
            SetScore(i, "Kills", killsTemp);
            SetScore(i, "Deaths", deathsTemp);

            GameObject playerEntry = Instantiate(playerEntryPrefab);
            playerEntry.transform.SetParent(this.transform);
            playerEntry.transform.Find("Player ID").GetComponent<Text>().text = idTemp.ToString();
            playerEntry.transform.Find("Kills").GetComponent<Text>().text = killsTemp.ToString();
            playerEntry.transform.Find("Deaths").GetComponent<Text>().text = deathsTemp.ToString();
        }
    }

    void SetScore(int id, string scoreType, int scoreValue)
    {
        scoreboard[id][scoreType] = scoreValue;
    }

    public IEnumerator EnableScoreboard()
    {
        gameObject.SetActive(true);
        yield return 0;
        gameObject.SetActive(false);
    }
}
