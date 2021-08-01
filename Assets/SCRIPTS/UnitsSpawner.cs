using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsSpawner : MonoBehaviour
{
    [SerializeField] private UnitsContainer _unitsContainer;
    [SerializeField] private Field _field;
    [Space]
    [SerializeField] private int _movingUnits;

    public static UnitsSpawner Self;

    private Unit _newUnit;

    public int MovingUnits { get => _movingUnits; private set => _movingUnits = value; }

    private void Awake()
    {
        if (Self == null)
            Self = this;
    }

    public void SpawnRandomUnit()
    {
        List<Cell> cellsForSpawn = _field.GetRandomEmptyCells();

        if (cellsForSpawn == null)
        {
            Debug.Log("No empty cells");
            return;
        }

        foreach (Cell cell in cellsForSpawn)
        {

            _newUnit = cell.SpawnUnit(_unitsContainer.GetRandomUnit(1, 2));

            _newUnit.MoveStart += OnUnitMovingStart;
            _newUnit.MoveEnd += OnUnitMovingEnd;
            _newUnit.Die += OnUnitDie;
        }
    }

    public void SpawnNextUnit(Cell cell, int level)
    {
        _newUnit = cell.SpawnUnit(_unitsContainer.GetUnit(level+1));

        _newUnit.MoveStart += OnUnitMovingStart;
        _newUnit.MoveEnd += OnUnitMovingEnd;
        _newUnit.Die += OnUnitDie;
    }

    private void OnUnitMovingStart()
    {
        _movingUnits++;
    }

    private void OnUnitMovingEnd()
    {
        _movingUnits--;

        if (_movingUnits == 0)
        {
            SpawnRandomUnit();
        }
    }

    private void OnUnitDie(Unit unit)
    {
        unit.MoveStart -= OnUnitMovingStart;
        unit.MoveEnd -= OnUnitMovingEnd;
        unit.Die -= OnUnitDie;
    }
}
