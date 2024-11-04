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
	[SerializeField] private float bulletLifeTime = 3f;
	//[SerializeField] private float rotateSpeed = 100f;

	private Transform target;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		SetRigidBodyDir();

	}
	public void SetTarget(Transform _target)
	{
		target = _target;
		Destroy(gameObject, bulletLifeTime); // Destroys bullet object via timer
	}
	private void FixedUpdate()
	{
		if (!target) return;

		// Calculates direction of target (Homing projectiles)
		Vector2 direction = (target.position - transform.position).normalized;
		rb.velocity = direction * bulletSpeed; // Updates bullet path
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
