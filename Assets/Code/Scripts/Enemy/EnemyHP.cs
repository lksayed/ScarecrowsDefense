using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHP : MonoBehaviour
{
	[Header("Attributes")]
	[SerializeField] public int healthPoints = 100;
	public void TakeDamage(int dmg)
	{
		healthPoints -= dmg;

		// Checks if enemy should be killed
		if (healthPoints <= 0)
		{
			// Tells "EnemySpawner" script an enemy object has been destroyed
			EnemySpawner.onEnemyDestroy.Invoke();
			Destroy(gameObject);
		}
	}
}

