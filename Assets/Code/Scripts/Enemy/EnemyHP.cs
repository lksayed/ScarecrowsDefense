using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHP : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public int healthPoints = 100;
    [SerializeField] public int moneyOnKill = 20;

    // Update is called once per frame
    void Update()
    {
        // Check if enemy should be killed
        if (healthPoints <= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
            LevelManager.main.IncreaseCurrency(moneyOnKill);
            return;
        }
    }
}
