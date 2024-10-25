using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Rigidbody2D rb;

	[Header("Attributes")]
	[SerializeField] private int bulletDamage = 50;
	[SerializeField] private float bulletSpeed = 5f;


	private Transform target;

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
	private void OnCollisionEnter2D(Collision2D other)
	{
		// Bullet finds enemy object's health script and applies damage to it
		other.gameObject.GetComponent<EnemyHP>().TakeDamage(bulletDamage);
		Destroy(gameObject); // Destroys bullet object upon collision
	}
}
