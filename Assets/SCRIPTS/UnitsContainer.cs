using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsContainer : MonoBehaviour
{
    [SerializeField] private List<Unit> _units = new List<Unit>();


    public Unit GetUnit(int nextLevel)
    {
        if (nextLevel < _units.Count)
        {
            return _units[nextLevel];
        }
        else
        {
            return _units[_units.Count-1];
        }
    }


    public Unit GetRandomUnit(int minLvl, int maxLvl)
    {
        int r = Random.Range(minLvl, maxLvl + 1);
        return _units[r];
    }
}
