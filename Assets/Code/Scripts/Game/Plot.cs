using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite hoverSprite;

	 private Sprite plotSprite;
	 private GameObject towerObj;

    public RangedTurret turret;
    public MeleeTurret turret2;
	 public SlowTurret turret3;
    public SplashTurret turret4;

	private void Awake()
    {
        plotSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
    private void OnMouseEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = hoverSprite;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = plotSprite;
    }

   private void OnMouseDown()
   {
      if (UIManager.main.IsHoveringUI()) return;

      if (towerObj != null)
      {
         if (towerObj.GetComponent<RangedTurret>())
         {
            turret.OpenUpgradeUI();
         }
			else if (towerObj.GetComponent<MeleeTurret>())
			{
				turret2.OpenUpgradeUI();
			}
			else if (towerObj.GetComponent<SlowTurret>())
         {
            turret3.OpenUpgradeUI();
         }
			else if (towerObj.GetComponent<SplashTurret>())
			{
				turret4.OpenUpgradeUI();
			}
			return;
      }

      Tower buildTower = BuildManager.main.GetSelectedTower();

      if (buildTower.cost > LevelManager.main.currency) // Checks if player has enough currency for purchase
      {
         return;
      }

      LevelManager.main.SpendCurrency(buildTower.cost); // Takes tower cost away from player's currency

      towerObj = Instantiate(buildTower.prefab, transform.position, Quaternion.identity);
      
      if (towerObj.GetComponent<RangedTurret>())
      {
         turret = towerObj.GetComponent<RangedTurret>();
      }
      else if (towerObj.GetComponent<MeleeTurret>())
      {
         turret2 = towerObj.GetComponent<MeleeTurret>();
      }
		else if (towerObj.GetComponent<SlowTurret>())
		{
			turret3 = towerObj.GetComponent<SlowTurret>();
		}
		else if (towerObj.GetComponent<SplashTurret>())
		{
			turret4 = towerObj.GetComponent<SplashTurret>();
		}
	}
}
