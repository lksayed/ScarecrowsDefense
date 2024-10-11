using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
}
