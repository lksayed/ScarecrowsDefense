using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Describes how enemies move along the path
public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    private float baseSpeed;

    private void Start()
    {
         baseSpeed = moveSpeed;
         target = LevelManager.main.path[pathIndex];
    }
    
    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Count)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        // target.position = POINT
        // transform.position = ENEMY
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }

	// Used for slowdown turrets
	public void UpdateSpeed(float newSpeed) 
   {
      moveSpeed = newSpeed;
   }

   public void ResetSpeed()
   {
      moveSpeed = baseSpeed;
   }
}
