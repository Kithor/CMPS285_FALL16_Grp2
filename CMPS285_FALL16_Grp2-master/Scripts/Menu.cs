using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Canvas MainCanvas;
    public Canvas OptionsCanvas;

    void Awake()
    {
        OptionsCanvas.enabled = false;
    }

    public void OptionsOn()
    {
        OptionsCanvas.enabled = true;
        MainCanvas.enabled = false;
    }

    public void ReturnOn()
    {
        OptionsCanvas.enabled = false;
        MainCanvas.enabled = true;
    }

   /* public void LoadOn()
    {
        SceneManager.LoadScene("MainCanvas");
    }*/

    bool isMute;

    public void Mute()
    {
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : 1;
    }

    public void ClickExit()
    {
        Application.Quit();
    }


}



