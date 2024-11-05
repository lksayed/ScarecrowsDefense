using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
   GameObject easyButton;
	GameObject medButton;
	GameObject hardButton;
	GameObject backButton;

	public static int level = 0;

	public void PlayEasy()
	{
		level = 0;
		SceneManager.LoadSceneAsync("Game");
	}

	public void PlayMedium()
	{
		level = 1;
        SceneManager.LoadSceneAsync("Game");
	}

	public void PlayHard()
	{
		level = 2;
		SceneManager.LoadSceneAsync("Game");
	}

	public void BackButton()
	{
		SceneManager.LoadSceneAsync("Main Menu");
	}
}
