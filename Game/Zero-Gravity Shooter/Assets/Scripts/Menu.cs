using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    //Declarations-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public Canvas MainCanvas;
    public Canvas OptionsCanvas;
    public Canvas ExitConfirmation;
    public Canvas ControlLayout;
    public Canvas Credits;
    //Main Menu Functions------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void Awake()  //when the program is started, the:
    {
        OptionsCanvas.enabled = false;      //Options screen is disabled
        ExitConfirmation.enabled = false;   //Confirmation of exit is disabled
        ControlLayout.enabled = false;      //Controls is disabled
        Credits.enabled = false;            //Credits is disabled
    }

    public void LoadOn()
    {
        SceneManager.LoadScene("Main");
    }

 
    public void OptionsOn()          //opens only the options screen
        {
            OptionsCanvas.enabled = true;
            MainCanvas.enabled = false;
        }
    
    public void ReturnOn()           //returns from options to main menu
        {
            OptionsCanvas.enabled = false;
            MainCanvas.enabled = true;
        }

    public void ClickExit()  //When exit is clicked:
        {
            MainCanvas.enabled = false;       //Main screen is disabled
            ExitConfirmation.enabled = true;  //Confirmation of exit is enabled and opened
        }
            public void ExitConfirmNo()       //returns the user to the main menu if "No" is selected
                {
                    ExitConfirmation.enabled = false;
                    MainCanvas.enabled = true;
                }

            public void ExitConfirmYes()      //closes the application if "yes" is selected
                {
                    Application.Quit();
                }

    //Options Canvas Functions-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void ControlsOn()         //opens only controls canvas
        {
            OptionsCanvas.enabled = false;
            ControlLayout.enabled = true;
        }

        public void ReturnFromControls() //returns from controls to options
            {
                ControlLayout.enabled = false;
                OptionsCanvas.enabled = true;
            }

    public void CreditsOn()         //opens only credits canvas
        {
            OptionsCanvas.enabled = false;
            Credits.enabled = true;
        }

        public void ReturnFromCredits() //returns from credits to options
            {
                OptionsCanvas.enabled = true;
                Credits.enabled = false;
            }

   /* bool isMute;

    public void Mute()
    {
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : 1;
    } */


}



