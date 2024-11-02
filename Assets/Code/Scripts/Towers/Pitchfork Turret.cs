using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Unity.VisualScripting;

public class PitchforkTurret : MonoBehaviour
{
	// Variables for "Ranged Turret"
	[Header("References")]
	[SerializeField] private Transform turretRotationPoint;
	[SerializeField] private Transform firingPoint;
	[SerializeField] private LayerMask enemyMask; // Allows turret to ignore map tiles
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private GameObject upgradeUI;
	[SerializeField] private Button upgradeButton;

	[Header("Attribute")]
	[SerializeField] private float targetingRange = 3f; // Turret Range
	[SerializeField] private float rotationSpeed = 200f; // Turret Rotation Speed
	[SerializeField] private float bps = 1f; // Bullets Per Second
	[SerializeField] private int baseUpgradeCost = 100; // Initial upgrade cost
	[SerializeField] private int levelCap = 3; // Tower upgrade cap

	private float bpsBase;
	private float targetingRangeBase;

	private Transform target;
	private float timeUntilFire;
	private int level = 1;

	private void Start()
	{
		// Initializes starting level stats of tower
		bpsBase = bps;
		targetingRangeBase = targetingRange;

		upgradeButton.onClick.AddListener(UpgradeTurret);

		//weaponSprite = this.transform.Find("Weapon").GetComponent<SpriteRenderer>();
	}
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
			timeUntilFire += Time.deltaTime;

			if (timeUntilFire >= 1f / bps) // (1 sec / bullets per second)
			{
				Shoot();
				timeUntilFire = 0f;
			}
		}
	}

	private void Shoot()
	{
		GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
		Pitchfork_Bullet bulletScript = bulletObj.GetComponent<Pitchfork_Bullet>();
		bulletScript.SetTarget(target); // Sets bullet's target to current locked-on target
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
	public void OpenUpgradeUI()
	{
		//if (level == levelCap) return;
		upgradeUI.SetActive(true);
	}
	public void CloseUpgradeUI()
	{
		upgradeUI.SetActive(false);
		UIManager.main.SetHoveringState(false);
	}

	private int CalculateCost() // Scales the cost of next upgrade
	{
		return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(level, 1f));
	}

	private float CalculateBPS() // Increases tower fire rate
	{
		return bpsBase * Mathf.Pow(level, 1f);
	}
	private float CalculateRange() // Increases tower firing range
	{
		return targetingRangeBase * Mathf.Pow(level, .2f);
	}

	public void UpgradeTurret() // Manages tower upgrade
	{
		// Checks if player has enough currency for upgrade
		if (CalculateCost() > LevelManager.main.currency) return;

		LevelManager.main.SpendCurrency(baseUpgradeCost);
		//Debug.Log("Upgrade Cost: " + baseUpgradeCost);

		// Assigns new stat values to tower
		level++;
		bps = CalculateBPS();
		targetingRange = CalculateRange();

		// Update upgrade cost
		baseUpgradeCost = CalculateCost();

		

		CloseUpgradeUI();
	}

}
