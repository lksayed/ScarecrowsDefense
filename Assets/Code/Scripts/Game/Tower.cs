using System;
using UnityEngine;

[Serializable]
public class Tower // Allow for towers to be selected in shop menu
{
   public string towerName;
   public int cost;
   public GameObject prefab;

   public Tower (string _towerName, int _cost, GameObject _prefab)
   {
      towerName = _towerName;
      cost = _cost;
      prefab = _prefab;
   }
}
