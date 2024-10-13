using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a singleton LevelManager
public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startingPoint;
    public Transform[] path;
 
    // Creates a single LevelManager on script load
    private void Awake()
    {
        main = this;
    }
}
