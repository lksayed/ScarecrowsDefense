using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Slow_Bullet : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private LayerMask enemyMask; 

	[Header("Attributes")]
	[SerializeField] private float bulletSpeed = 5f;
	[SerializeField] private float bulletLifeTime = 3f;
	[SerializeField] private float rotateSpeed = 100f;
	[SerializeField] private float spreadRange = 1f; // Bullet Area-of-Effect
	//[SerializeField] private float slowTime = 1f; // Slow-effect time in secs

	private Transform target;
	public void SetTarget(Transform _target)
	{
		target = _target;
	}

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		SetRigidBodyDir();

	}
	private void FixedUpdate()
	{
		if (!target) return;

		// Calculates direction of target (Homing projectiles)
		Vector2 direction = (target.position - transform.position).normalized;

		// Calculates cross-product of bullet (Bullet Direction * Target Direction)
		// This allows bullet to rotate in flight.
		float rotateAmount = Vector3.Cross(direction, transform.up).z;
		rb.angularVelocity = -rotateAmount * rotateSpeed; // Updates bullet in-flight rotation
		rb.velocity = direction * bulletSpeed; // Updates bullet speed
		Destroy(gameObject, bulletLifeTime); // Destroys bullet after 'n' secs
	}
	private void FreezeEnemies()
	{
		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, spreadRange,
		(Vector2)transform.position, 0f, enemyMask);

		if (hits.Length > 0)
		{
			for (int i = 0; i < hits.Length; i++)
			{
				RaycastHit2D hit = hits[i];

				EnemyMovement enemyMovement = hit.transform.GetComponent<EnemyMovement>();
				enemyMovement.UpdateSpeed(0.8f);
				//StartCoroutine(ResetEnemySpeed(enemyMovement)); // May be removed
			}
		}
	}
	/*
	private IEnumerator ResetEnemySpeed(EnemyMovement enemyMovement)
	{
		yield return new WaitForSeconds(slowTime);
		enemyMovement.ResetSpeed();
	}*/
	private void SetRigidBodyDir() // Sets the firing direction of bullet.
	{
		float angle = Mathf.Atan2(target.position.y - transform.position.y,
		target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

		Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
		rb.transform.rotation = targetRotation;
	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		FreezeEnemies();
		Destroy(gameObject);
	}

	#if UNITY_EDITOR
	private void OnDrawGizmosSelected() // Display gizmos of turret (Only in editor)
	{
		Handles.color = Color.blue;
		Handles.DrawWireDisc(transform.position, transform.forward, spreadRange);
	}
	#endif
}
