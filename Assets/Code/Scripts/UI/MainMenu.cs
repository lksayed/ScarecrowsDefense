using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
   GameObject playButton;
   GameObject helpButton;
   GameObject quitButton;
   GameObject backButton;
   GameObject instructions;
	public void PlayGame()
   {
      // Loads a specified scene after user clicks button
      // You can find scenes in Unity through "File -> Build Settings -> Scenes In Build"
      // For below: Either use scene index shown in "Scenes In Build" window or
      // string of scene name.
      SceneManager.LoadSceneAsync("Level Select");
   }
	public void QuitGame()
   {
      // Enables the game to be exited
      Application.Quit();
   }

   public void OpenHelpScreen()
    {
        playButton.SetActive(false);
        helpButton.SetActive(false);
        quitButton.SetActive(false);
        backButton.SetActive(true);
        instructions.SetActive(true);
    }

    public void CloseHelpScreen()
    {
        playButton.SetActive(true);
        helpButton.SetActive(true);
        quitButton.SetActive(true);
        backButton.SetActive(false);
        instructions.SetActive(false);
    }


    private void Awake()
    {
       playButton = GameObject.Find("Play");
       helpButton = GameObject.Find("How To Play");
       quitButton = GameObject.Find("Exit");
       backButton = GameObject.Find("Back");
       instructions = GameObject.Find("InstructionText");

		 backButton.SetActive(false);
       instructions.SetActive(false);
    }
}
