using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
	// Variables for "Turret"
	[Header("References")]
	[SerializeField] private Transform turretRotationPoint;
	[SerializeField] private LayerMask enemyMask; // Allows turret to ignore map tiles

	[Header("Attribute")]
	[SerializeField] private float targetingRange = 3f; // Turret Range
	[SerializeField] private float rotationSpeed= 200f; // Turret Rotation Speed

	private Transform target;

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
	}
	private void FindTarget() // The Tower's target finder
	{
		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange,
		(Vector2)transform.position, 0f, enemyMask);

		if (hits.Length > 0) 
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
	private void OnDrawGizmosSelected() // Display gizmos of turret (Only in editor)
	{
		Handles.color = Color.blue;
		Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
	}

}
