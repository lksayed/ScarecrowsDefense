using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Unity.VisualScripting;

public class SplashTurret : MonoBehaviour
{
	// Variables for "Ranged Turret"
	[Header("References")]
	[SerializeField] private Transform turretRotationPoint;
	[SerializeField] private Transform firingPoint;
	[SerializeField] private LayerMask enemyMask; // Allows turret to ignore map tiles
	[SerializeField] private GameObject bulletPrefab;
	[SerializeField] private GameObject bulletUpgrade;
	[SerializeField] private GameObject bulletUpgradeII;
	[SerializeField] private GameObject selectUI;
	[SerializeField] private Button upgradeButton;
	[SerializeField] private Sprite upgradeBullet; // First Upgrade Sprite
	[SerializeField] private Sprite upgradeBulletII; // Second Upgrade Sprite

	[Header("Attribute")]
	[SerializeField] private float targetingRange = 4.5f; // Turret Range
	[SerializeField] private float rotationSpeed = 250f; // Turret Rotation Speed
	[SerializeField] private float bps = 0.5f; // Bullets Per Second
	[SerializeField] private int baseUpgradeCost = 500; // Initial upgrade cost
	[SerializeField] private int levelCap = 3; // Tower upgrade cap

	private SpriteRenderer weaponSprite;
	private Transform target;
	private float timeUntilFire;
	private float targetingRangeBase;
	private float bpsBase;
	private int level = 1;

	private void Start()
	{
		// Initializes starting level stats of tower
		bpsBase = bps;
		targetingRangeBase = targetingRange;

		upgradeButton.onClick.AddListener(UpgradeTurret);

		// Finds "Weapon" sprite in parent object (the tower)
		weaponSprite = this.transform.Find("RotationPoint").Find("Weapon").GetComponent<SpriteRenderer>();
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

			if (timeUntilFire >= 1f / bps)
			{
				Shoot();
				timeUntilFire = 0f;
			}
		}
	}

	private void Shoot()
	{
		GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
		Splash_Bullet bulletScript = bulletObj.GetComponent<Splash_Bullet>();
		bulletScript.SetTarget(target); // Sets bullet's target to current target
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
	#if UNITY_EDITOR
	private void OnDrawGizmosSelected() // Display gizmos of turret (Only in editor)
	{
		Handles.color = Color.blue;
		Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
	}
#endif
	public void OpenUpgradeUI()
	{
		if (level == levelCap) return;
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
			bps = 0.75f;
			targetingRange = 5f;

			// Switches respective sprites
			bulletPrefab = bulletUpgrade;
			weaponSprite.sprite = upgradeBullet;

			// Update upgrade cost
			baseUpgradeCost = 800;
		}

		if (level == 3)
		{
			// Assigns new stat values to tower
			bps = 1f;
			targetingRange = 5.5f;

			// Switches respective sprites
			bulletPrefab = bulletUpgradeII;
			weaponSprite.sprite = upgradeBulletII;

			// Diables upgrade button once tower upgrade reaches max
			upgradeButton.interactable = false;
		}

		CloseUpgradeUI();
	}
}
