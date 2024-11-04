using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Splash_Bullet : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private LayerMask enemyMask;

	[Header("Attributes")]
	[SerializeField] private int bulletDamage = 150;
	[SerializeField] private float bulletSpeed = 5f;
	[SerializeField] private float bulletLifeTime = 3f;
	[SerializeField] private float rotateSpeed = 100f;
	[SerializeField] private float spreadRange = 1f; // Bullet Area-of-Effect

	private Transform target;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		SetRigidBodyDir();

	}
	public void SetTarget(Transform _target)
	{
		target = _target;
		//Destroy(gameObject, 3f); // Destroys bullet object via timer
	}
	private void FixedUpdate()
	{
		if (!target) return;

		// Calculates direction of target (Homing projectiles)
		Vector2 direction = (target.position - transform.position).normalized;
		rb.velocity = direction * bulletSpeed; // Updates bullet path
	}
	private void SplashEnemies()
	{
		// Find enemies in bullet's range and does damage to them
		Collider2D[] enemy = Physics2D.OverlapCircleAll(target.position, spreadRange, enemyMask);
		for (int i = 0; i < enemy.Length; i++) // Applies damage to all enemies within range
		{
			enemy[i].GetComponent<EnemyHP>().TakeDamage(bulletDamage);
		}

	}
	private void SetRigidBodyDir() // Sets the firing direction of bullet.
	{
		float angle = Mathf.Atan2(target.position.y - transform.position.y,
		target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

		Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
		rb.transform.rotation = targetRotation;
	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		SplashEnemies();
		Destroy(gameObject); // Destroys bullet object upon collision
	}

#if UNITY_EDITOR
	private void OnDrawGizmosSelected() // Display gizmos of turret (Only in editor)
	{
		Handles.color = Color.blue;
		Handles.DrawWireDisc(transform.position, transform.forward, spreadRange);
	}
#endif

}
