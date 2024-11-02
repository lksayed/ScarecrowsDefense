using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// This script will make it so the mouse cursor does not click through UI
// Also closes it once mouse hovers away from UI background
public class UpgradeUIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public bool mouse_over = false;
	public void OnPointerEnter(PointerEventData eventData)
	{
		mouse_over = true;
		UIManager.main.SetHoveringState(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mouse_over = false;
		UIManager.main.SetHoveringState(false);
		gameObject.SetActive(false);
	}
}
