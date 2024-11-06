using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite invalidSprite;

    private Sprite plotSprite;
	private GameObject towerObj;

    public RangedTurret turret;
    public MeleeTurret turret2;
	public SlowTurret turret3;
    public SplashTurret turret4;

    public bool isValid = true;

	private void Awake()
    {
        plotSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    private void Start()
    {
        // Determine if this plot is invalid
        if (LevelSelect.level == 0)
        {
            for (int i = 0; i < LevelManager.main.easyInvalidPlots.Count; i++)
            {
                if (gameObject.name == LevelManager.main.easyInvalidPlots[i].name)
                {
                    isValid = false;
                    break;
                }
            }
        }
        else if (LevelSelect.level == 1)
        {
            for (int i = 0; i < LevelManager.main.mediumInvalidPlots.Count; i++)
            {
                if (gameObject.name == LevelManager.main.mediumInvalidPlots[i].name)
                {
                    isValid = false;
                    break;
                }
            }
        }
        else if (LevelSelect.level == 2)
        {
            for (int i = 0; i < LevelManager.main.hardInvalidPlots.Count; i++)
            {
                if (gameObject.name == LevelManager.main.hardInvalidPlots[i].name)
                {
                    isValid = false;
                    break;
                }
            }
        }
    }
    private void OnMouseEnter()
    {
        if (isValid)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = hoverSprite;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = invalidSprite;
        }
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = plotSprite;
    }

   private void OnMouseDown()
   {
      if (UIManager.main.IsHoveringUI()) return;
      if (EventSystem.current.IsPointerOverGameObject()) return;

      if (!isValid) return;

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
