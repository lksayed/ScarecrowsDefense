using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    private Sprite plotSprite;
    [SerializeField] private Sprite hoverSprite;
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

    private void OnMouseOver()
    {
        // User must RIGHT-CLICK to place troops
        if (Input.GetMouseButtonDown(1))
        {
            if (tower != null) return;
            Tower towerToBuild = BuildManager.main.GetSelectedTower();

            if (towerToBuild.cost > LevelManager.main.currency)
            {
                Debug.Log("You can't afford this tower");
                return;
            }

            LevelManager.main.SpendCurrency(towerToBuild.cost);
            tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
        }
    }
}
