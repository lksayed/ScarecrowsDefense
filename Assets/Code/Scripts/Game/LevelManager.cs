using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// Create a singleton LevelManager
public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Sprite[] backgroundSprites;

    public Transform easyStartingPoint;
    public Transform mediumStartingPoint;
    public Transform hardStartingPoint;
    public Transform[] easyPath;
    public Transform[] mediumPath;
    public Transform[] hardPath;
    public Transform startingPoint;
    public List<Transform> path;

    public List<GameObject> easyInvalidPlots;
    public List<GameObject> mediumInvalidPlots;
    public List<GameObject> hardInvalidPlots;

    public int playerHP;
    public int currency;

    // Creates a single LevelManager on script load
    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        // NOTE: This overrides the custom values set in the Unity Inpector Window!
        // currency = 500;
        // playerHP = 50;

        GameObject background = GameObject.Find("BackgroundSprite");

        if (LevelSelect.level == 0)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[0];
            for (int i = 0; i < easyPath.Length; i++)
            {
                path.Add(easyPath[i]);
            }
            startingPoint = easyStartingPoint;
        }
        else if (LevelSelect.level == 1)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[1];
            for (int i = 0; i < mediumPath.Length; i++)
            {
                path.Add(mediumPath[i]);
            }
            startingPoint = mediumStartingPoint;
        }
        else if (LevelSelect.level == 2)
        {
            background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[2];
            for (int i = 0; i < hardPath.Length; i++)
            {
                path.Add(hardPath[i]);
            }
            startingPoint = hardStartingPoint;
        }
        else 
        {
            Debug.Log("Error: Failed to load a level!");
        }
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount; // Buy Item
            return true;
        }
        else
        {
			// FIXME: Display message to notify player they don't have enough money for purchase
			return false; 
        }
    }

    public int GetCurrentWave()
    {
        return GetComponent<EnemySpawner>().currentWave;
    }
    public void Update()
    {
        if (playerHP <= 0)
        {
            SceneManager.LoadScene("Lose");
        }

        if (GetComponent<EnemySpawner>().currentWave > 10)
        {
            SceneManager.LoadScene("Win");
        }

        if (Input.GetKeyDown(KeyCode.X)) // Cheat Money Key
        {
            IncreaseCurrency(1000);
        }
    }
}
