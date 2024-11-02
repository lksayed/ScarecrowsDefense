using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Create a singleton LevelManager
public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startingPoint;
    public Transform[] path;

    public int playerHP;
    public int currency;

    // Creates a single LevelManager on script load
    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        currency = 2500;
        playerHP = 50;
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
    }
}
