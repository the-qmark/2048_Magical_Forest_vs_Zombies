using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cell : MonoBehaviour
{
    public bool IsEmpty { get => _units.Count == 0; private set => IsEmpty = value; }
    public bool IsMerge { get => _units.Count == 2; private set => IsMerge = value; }
    public int UnitLevel { get => _units.Count == 0 ? 0 : _units[0].Level; private set => UnitLevel = value; }

    [SerializeField] private List<Unit> _units = new List<Unit>();

    public event UnityAction<Cell, int> Merge;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsEmpty)
        {
            transform.localScale = new Vector3(0.5f, 0.1f, 0.5f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 0.1f, 1f);
        }
    }

    public Unit SpawnUnit(Unit unitForSpawn)
    {
        if (unitForSpawn == null)
            Debug.LogWarning("NULL");

        Unit spawnedUnit = Instantiate(unitForSpawn, transform.position, Quaternion.identity);

        if (spawnedUnit == null)
            Debug.LogWarning("NULL2");

        spawnedUnit.SetParentCell(this);

        AddUnit(spawnedUnit);

        return spawnedUnit;
    }

    public void MoveUnit(Cell cellToMove)
    {
        if (_units.Count == 0)
           Debug.LogError($"{name} - NULL");

        _units[0].MoveTo(cellToMove);
        cellToMove.AddUnit(_units[0]);
        _units.Clear();
    }

    private void AddUnit(Unit newUnit)
    {
        _units.Add(newUnit);

        if (_units.Count == 2)
        {
            //IsMerge = true;
            _units[1].MoveEnd += DoMerge;
        }
        else if (_units.Count > 2) /// DELETE??
        {
            Debug.LogWarning("Something go wrong!!!!!!!!!!!!!!!!!!!!!!!");
        }
    }

    private void DoMerge()
    {
        _units[1].MoveEnd -= DoMerge;

        int lastLevel = UnitLevel;

        foreach (Unit unit in _units)
            Destroy(unit.gameObject);

        _units.Clear();

        Merge?.Invoke(this, lastLevel);
    }
}
