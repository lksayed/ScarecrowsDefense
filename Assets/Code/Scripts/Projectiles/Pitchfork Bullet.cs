using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchforkBullet : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Rigidbody2D rb;

	[Header("Attributes")]
	[SerializeField] private float bulletSpeed = 5f;
	[SerializeField] private int bulletDamage = 50;
	[SerializeField] private float rotateSpeed = 100f;

	private Transform target;
	public void SetTarget(Transform _target)
	{
		target = _target;
	}

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	private void FixedUpdate()
	{
		if (!target) return;

		// Calculates direction of target (Homing projectiles)
		Vector2 direction = (Vector2)target.position - rb.position;
		direction.Normalize();

		// Calculates cross-product of bullet vectors
		float rotateAmount = Vector3.Cross(direction, transform.up).z; 

		rb.angularVelocity = -rotateAmount * rotateSpeed; // Updates bullet rotation speed 
		rb.velocity = transform.up * bulletSpeed; // Updates bullet speed
		Destroy(gameObject, 6f); // Destroys bullet after 6 secs
	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		// Bullet finds enemy object's health script and applies damage to it
		other.gameObject.GetComponent<EnemyHP>().TakeDamage(bulletDamage);
		Destroy(gameObject); // Destroys bullet object upon collision
	}
}
