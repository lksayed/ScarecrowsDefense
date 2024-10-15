using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHP : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public int hp = 100;

    // Update is called once per frame
    void Update()
    {
        // Check if enemy should be killed
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
