using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite hoverSprite;

	 private Sprite plotSprite;
	 private GameObject tower;

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
      if (tower != null) return;

         Tower buildTower = BuildManager.main.GetSelectedTower();

         if (buildTower.cost > LevelManager.main.currency) // Checks if player has enough currency for purchase
         {
            return;
         }

         LevelManager.main.SpendCurrency(buildTower.cost); // Takes tower cost away from player's currency

         Instantiate(buildTower.prefab, transform.position, Quaternion.identity);
    }
}
