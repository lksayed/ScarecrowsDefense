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
      SceneManager.LoadSceneAsync(1); // Uses scene index or name
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
