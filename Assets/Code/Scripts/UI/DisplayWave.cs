using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWave : MonoBehaviour
{
    public TMPro.TextMeshProUGUI wave;

    // Update is called once per frame
    void Update()
    {
        wave.text = LevelManager.main.GetCurrentWave().ToString();
    }
}
