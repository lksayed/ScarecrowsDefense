using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class Base_Turret : MonoBehaviour
{
	// Variables for "Ranged Turret"
	[Header("References")]
	[SerializeField] private Transform turretRotationPoint;
	[SerializeField] private LayerMask enemyMask; // Allows turret to ignore map tiles

	[Header("Attribute")]
	[SerializeField] private float targetingRange = 3f; // Turret Range
	[SerializeField] private float rotationSpeed = 200f; // Turret Rotation Speed
	[SerializeField] private float aps = 1f; // Attacks Per Second (Can be modified)

	private Transform target;
	private float timeBtwAtk;

	public float startTimeBtwAtk;

	private void Update()
	{
		if (target == null)
		{
			FindTarget();
			return;
		}

		RotateToTarget();

		if (!CheckTargetInRange())
		{
			target = null;
		}
		else
		{
			timeBtwAtk += Time.deltaTime; // Increments a timer for attacks in real time

			if (timeBtwAtk >= 1f / aps)
			{
				// This portion of code should be modified
				timeBtwAtk = 0f;
			}
		}
	}

	private void FindTarget() // The Tower's target finder
	{
		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange,
		(Vector2)transform.position, 0f, enemyMask);

		if (hits.Length > 0) // Note: Could be modified for target prioritization
		{
			target = hits[0].transform; // Gives transform of first enemy in range
		}
	}
	private bool CheckTargetInRange() // Checks if current locked-on target is in range
	{
		return Vector2.Distance(target.position, transform.position) <= targetingRange;
	}
	private void RotateToTarget() // Allows the turret to rotate towards a target
	{
		float angle = Mathf.Atan2(target.position.y - transform.position.y,
		target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

		Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
		turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation,
		targetRotation, rotationSpeed * Time.deltaTime); // Time.deltaTime ensures rotate speed remain constant
	}
#if UNITY_EDITOR
	private void OnDrawGizmosSelected() // Display gizmos of turret (Only in editor)
	{
		Handles.color = Color.blue;
		Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
	}
#endif
}
