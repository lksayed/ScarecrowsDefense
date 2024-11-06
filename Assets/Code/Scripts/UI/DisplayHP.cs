using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHP : MonoBehaviour
{
    public TMPro.TextMeshProUGUI hp;
    
    // Update is called once per frame
    void Update()
    {
        hp.text = LevelManager.main.playerHP.ToString();
    }
}
