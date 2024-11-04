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

	public void PlayEasy()
	{
		SceneManager.LoadSceneAsync("Easy Map");
	}

	public void PlayMedium()
	{
		SceneManager.LoadSceneAsync("Medium Map");
	}

	public void PlayHard()
	{
		SceneManager.LoadSceneAsync("Hard Map");
	}

	public void BackButton()
	{
		SceneManager.LoadSceneAsync("Main Menu");
	}
}
