using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitchfork_Bullet : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Rigidbody2D rb;

	[Header("Attributes")]
	[SerializeField] private int bulletDamage = 50;
	[SerializeField] private float bulletSpeed = 5f;
	[SerializeField] private float bulletLifeTime = 2f;
	[SerializeField] private float rotateSpeed = 250f;

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
	private void SetRigidBodyDir() // Sets the firing direction of bullet.
	{
		float angle = Mathf.Atan2(target.position.y - transform.position.y,
		target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

		Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
		rb.transform.rotation = targetRotation;
	}
	private void OnCollisionEnter2D(Collision2D other)
	{
		// Bullet finds enemy object's health script and applies damage to it
		other.gameObject.GetComponent<EnemyHP>().TakeDamage(bulletDamage);
		Destroy(gameObject); // Destroys bullet object upon collision
	}
}
