using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private int buttonWidth = 200;
    private int buttonHeight = 50;
    private int groupWidth = 200;
    private int groupHeight = 170;
    public static bool paused = false;
    CursorLockMode locked;

	void Start ()
    {
        Cursor.lockState = locked;
        Cursor.visible = (CursorLockMode.Locked != locked);
        Time.timeScale = 1;
	}
	

    void OnGUI()
    {
        if(paused)
        {
            GUI.BeginGroup(new Rect(((Screen.width / 2) - (groupWidth / 2)),((Screen.height / 2) - (groupHeight / 2)), groupWidth, groupHeight));
            if(GUI.Button(new Rect(0,0,buttonWidth,buttonHeight) , "Resume"))
            {
                if (paused)
                {
                    paused = false;
                    Time.timeScale = 1;
                }
                else
                {
                    paused = true;
                    Time.timeScale = 1;
                }
            }
            if(GUI.Button(new Rect(0, 60, buttonWidth, buttonHeight), "Main Menu"))
            {
                SceneManager.LoadScene("Main Menu");
            }
            if (GUI.Button(new Rect(0, 120, buttonWidth, buttonHeight), "Exit Game"))
            {
                Application.Quit();
            }
            GUI.EndGroup();
            
        }

    }

	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (paused)
            {
                paused = false;
                Time.timeScale = 1;
            }
            else
            {
                paused = true;
                Time.timeScale = 1;
            }
        }
    }
}

