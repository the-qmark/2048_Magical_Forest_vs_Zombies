using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsContainer : MonoBehaviour
{
    //public static UnitsContainer Self;
    
    [SerializeField] private List<Unit> _units = new List<Unit>();

    private void Awake()
    {
        //if (Self == null)
        //    Self = this;
    }

    //public Unit GetNextUnit(int level)
    //{
    //    if (level == _units.Count)
    //    {
    //        return _units[level - 1];
    //    }
    //    else
    //    {
    //        return _units[level];
    //    }
    //}

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
