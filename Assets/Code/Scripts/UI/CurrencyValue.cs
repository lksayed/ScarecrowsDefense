using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyValue : MonoBehaviour
{
    public TextMeshProUGUI txt;
    // Start is called before the first frame update
    void Start()
    {
        txt.text = LevelManager.main.currency.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = LevelManager.main.currency.ToString();
    }
}
