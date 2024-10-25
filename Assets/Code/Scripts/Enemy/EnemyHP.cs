using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHP : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public int healthPoints = 100;
    [SerializeField] public int moneyOnKill = 20;

    public void TakeDamage(int damage)
   {
      healthPoints -= damage;
   }

    // Update is called once per frame
    void Update()
    {
		// Tells "EnemySpawner" script an enemy object has been destroyed
		if (healthPoints <= 0)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
            LevelManager.main.IncreaseCurrency(moneyOnKill);
            return;
        }
    }
}
