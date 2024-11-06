using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWave : MonoBehaviour
{
    GameObject button;
    EnemySpawner reference;

    private void Start()
    {
        button = GameObject.Find("Start");
    }
    public void GameStart()
    {
        reference = LevelManager.main.GetComponent<EnemySpawner>();
        reference.StartWaveButton();
        button.SetActive(false);
    }
}
