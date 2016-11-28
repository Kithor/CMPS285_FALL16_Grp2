using UnityEngine;
using UnityEngine.UI;

public class ScoreList : MonoBehaviour
{
    ScoreManager scoreManager;

    public GameObject playerEntryPrefab;

	void Start ()
    {
        scoreManager = GetComponentInParent<ScoreManager>();
    }
	
	void Update ()
    {
        while(transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }

        int playerIDs = scoreManager.GetPlayerIDs();

        for(int i = 1; i <= playerIDs; i++)
        {
            GameObject playerEntry = Instantiate(playerEntryPrefab);
            playerEntry.transform.SetParent(transform);

            playerEntry.transform.Find("Player ID").GetComponent<Text>().text = i.ToString();
            playerEntry.transform.Find("Kills").GetComponent<Text>().text = scoreManager.GetScore(i, "Kills").ToString();
            playerEntry.transform.Find("Deaths").GetComponent<Text>().text = scoreManager.GetScore(i, "Deaths").ToString();
        }
    }
}
