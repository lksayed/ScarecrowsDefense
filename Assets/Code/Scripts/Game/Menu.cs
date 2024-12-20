using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
	[SerializeField] Animator anim;

	private bool isMenuOpen = true;

	public void ToggleMenu()
    {
		isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }
    private void OnGUI() // Updates currency UI text in game
	{
		currencyUI.text = LevelManager.main.currency.ToString();
	}
	public void SetSelected()
	{

	}
}
