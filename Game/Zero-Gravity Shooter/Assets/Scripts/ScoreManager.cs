using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    GameObject player;
    GameObject playerEntry;
    Dictionary<int, Dictionary<string, int>> scoreboard;
    int idTemp;
    int killsTemp;
    int deathsTemp;

    void Start()
    {
        scoreboard = new Dictionary<int, Dictionary<string, int>>();
    }

    void Init()
    {
        if (scoreboard != null)
            return;

        scoreboard = new Dictionary<int, Dictionary<string, int>>();
    }

    public void AddScore(int id, int scoreValueKills, int scoreValueDeaths)
    {
        Init();
        scoreboard[id] = new Dictionary<string, int>();

        scoreboard[id]["Player ID"] = id;
        scoreboard[id]["Kills"] = scoreValueKills;
        scoreboard[id]["Deaths"] = scoreValueDeaths;
    }

    public void UpdateScore(int id, string scoreType, int scoreValue)
    {
        scoreboard[id][scoreType] = scoreValue;
    }

    public int GetScore(int id, string scoreType)
    {
        return scoreboard[id][scoreType];
    }

    public int GetPlayerIDs()
    {
        Init();
        return scoreboard.Keys.ToArray().Length; 
    }
}
