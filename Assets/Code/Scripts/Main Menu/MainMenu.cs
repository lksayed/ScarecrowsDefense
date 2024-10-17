using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
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

   public void HelpScreen()
    {
        // Hide buttons and show a wall of text
        GameObject playButton = GameObject.Find("Play");
        GameObject helpButton = GameObject.Find("How To Play");
        GameObject quitButton = GameObject.Find("Exit");
        GameObject backButton = GameObject.Find("Back");

        playButton.SetActive(false);
        helpButton.SetActive(false);
        quitButton.SetActive(false);    
        backButton.SetActive(false);

    }

    private void Awake()
    {
        GameObject backButton = GameObject.Find("Back");
        backButton.SetActive(false);
    }
}
