using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Unity.VisualScripting;

public class MeleeTurret : MonoBehaviour
{
	// Variables for "Melee Turret"
	[Header("References")]
	[SerializeField] private Transform turretRotationPoint;
	[SerializeField] private LayerMask enemyMask; // Allows turret to ignore map tiles
	[SerializeField] private GameObject selectUI;
	[SerializeField] private GameObject rangeI;
	[SerializeField] private GameObject rangeII;
	[SerializeField] private Button sellButton;
	[SerializeField] private Button upgradeButton;
	[SerializeField] private Sprite upgradeWeapon;

	[Header("Attribute")]
	[SerializeField] private int meleeDamage = 100; // Turret damage
	[SerializeField] private float targetingRange = 1f; // Turret Range
	[SerializeField] private float rotationSpeed = 200f; // Turret Rotation Speed
	[SerializeField] private float aps = 1f; // Attacks Per Second (Can be modified)
	[SerializeField] private int baseUpgradeCost = 175; // Initial upgrade cost

	private SpriteRenderer weaponSprite;
	private Transform target;
	private Animator meleeAnimator;
	private float timeBtwAtk;
	private float targetingRangeBase;
	private float apsBase;
	private int level = 1;

	private void Start()
	{
		// Initializes starting level stats of tower
		apsBase = aps;
		targetingRangeBase = targetingRange;

		upgradeButton.onClick.AddListener(UpgradeTurret);
		sellButton.onClick.AddListener(SellTurret);

		// Finds "Weapon" sprite in parent object (the tower)
		weaponSprite = this.transform.Find("RotationPoint").Find("Scythe").GetComponent<SpriteRenderer>();
		meleeAnimator = GetComponent<Animator>();
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
			timeBtwAtk += Time.deltaTime; // Increments a timer for attacks in real time

			if (timeBtwAtk >= 1f / aps)
			{
				Attack();
				timeBtwAtk = 0f;
			}
		}
	}
	private void Attack()
	{
		Collider2D[] enemy = Physics2D.OverlapCircleAll(target.position, targetingRange, enemyMask);
		for (int i = 0; i < enemy.Length; i++) // Applies damage to all enemies within range
		{
			enemy[i].GetComponent<EnemyHP>().TakeDamage(meleeDamage);
		}
		meleeAnimator.SetTrigger("Attack"); // Animation makes it look like scythe does damage
		//Debug.Log("Scythe Attack");
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
		selectUI.SetActive(true);
	}
	public void CloseUpgradeUI()
	{
		selectUI.SetActive(false);
		UIManager.main.SetHoveringState(false);
	}

	public void UpgradeTurret() // Manages tower upgrade
	{
		// Checks if player has enough currency for upgrade
		if (baseUpgradeCost > LevelManager.main.currency) return;

		LevelManager.main.SpendCurrency(baseUpgradeCost);
		//Debug.Log("Upgrade Cost: " + baseUpgradeCost);

		level++;

		// Switches bullet prefabs per upgrade levels
		if (level == 2)
		{
			// Assigns new stat values to tower
			meleeDamage = 200;
			aps = 2f;
			targetingRange = 1.5f;

			// Switches weapon sprite
			weaponSprite.sprite = upgradeWeapon;

			// Switch Range Circle
			rangeI.SetActive(false);
			rangeII.SetActive(true);

			// Update upgrade cost
			baseUpgradeCost = 263;

			// Diables upgrade button once tower upgrade reaches max
			upgradeButton.interactable = false;
		}

		CloseUpgradeUI();
	}
	public void SellTurret()
	{
		// This would give player half of total currency spent on this tower
		// regardless if player upgraded it or not.
		LevelManager.main.currency += baseUpgradeCost;

		Destroy(gameObject);
		CloseUpgradeUI();
	}
}
