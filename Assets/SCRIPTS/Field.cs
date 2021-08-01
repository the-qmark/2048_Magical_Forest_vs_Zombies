using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private int _size;
    [SerializeField] private float _space;
    [SerializeField] private Cell _cellPrefab;
    [Space]
    [SerializeField] private int _maxCountOfMoves;
    [Space]
    [SerializeField] private UnitsSpawner _unitsSpawner;
    
    private int _currentCountOfMoves;

    private bool _isMove;

    private Cell[,] _cells;


    private void Start()
    {
        _currentCountOfMoves = _maxCountOfMoves;
        GenerateCells();
        _unitsSpawner.SpawnRandomUnit(); 
    }


    private void GenerateCells()
    {
        _cells = new Cell[_size, _size];

        float sizeOfCell = _cellPrefab.transform.localScale.x;

        float startPosX = transform.position.x - sizeOfCell - (_space * (float)(_size - 1) / 2) - (_space * (float)(_size - 3));
        float startPosZ = transform.position.z + sizeOfCell + (_space * (float)(_size - 1) / 2) + (_space * (float)(_size - 3));

        float posX = startPosX;
        float posZ = startPosZ;

        for (int row = 0; row<_size; row++)
        {
	        for (int column = 0; column<_size; column++)
	        {
		        Vector3 pos = new Vector3(posX, transform.position.y, posZ);
                Cell newCell = Instantiate(_cellPrefab, pos, Quaternion.identity, transform);
                _cells[row, column] = newCell;
		        newCell.name = $"[{row};{column}]";
                newCell.Merge += OnCellMerge;
		        posX += sizeOfCell + _space;
	        }
            posX = startPosX;
            posZ -= (sizeOfCell + _space);
        }
    }

    public List<Cell> GetRandomEmptyCells()
    {
        List<Cell> allEmptyCells = new List<Cell>();

        foreach (Cell cell in _cells)
            if (cell.IsEmpty)
                allEmptyCells.Add(cell);

        List<Cell> emptyCellsForSpawn = new List<Cell>();

        if (allEmptyCells.Count == 1)
        {
            emptyCellsForSpawn.Add(allEmptyCells[0]);
            return emptyCellsForSpawn;
        }
        else if (allEmptyCells.Count > 1)
        {
            int rnd = Random.Range(0, allEmptyCells.Count);
            emptyCellsForSpawn.Add(allEmptyCells[rnd]);
            allEmptyCells.RemoveAt(rnd);
            rnd = Random.Range(0, allEmptyCells.Count);
            emptyCellsForSpawn.Add(allEmptyCells[rnd]);

            return emptyCellsForSpawn;
        }
        else
        {
            return null;
        }
    }

    public void Move(Vector2 direction)
    {
        if (_currentCountOfMoves == 0)
        {
            return;
        }

        if (_unitsSpawner.MovingUnits != 0)
            return;

        if (direction.x > 0) // вправо
        {
            for (int rowIndex = 0; rowIndex < _size; rowIndex++) // идет свеху вниз
            {
                for (int columnIndex = _size - 2; columnIndex >= 0; columnIndex--) // 2 1 0
                {
                    if (!_cells[rowIndex, columnIndex].IsEmpty)
                    {
                        int nextCol = columnIndex;

                        for (int colDelta = columnIndex + 1; colDelta < _size; colDelta++)
                        {
                            if (_cells[rowIndex, colDelta].IsEmpty)
                            {
                                nextCol = colDelta;
                            }
                            else if (_cells[rowIndex, colDelta].UnitLevel == _cells[rowIndex, columnIndex].UnitLevel && !_cells[rowIndex, colDelta].IsMerge)
                            {
                                nextCol = colDelta;
                                break;
                            }
                            else if (!_cells[rowIndex, colDelta].IsEmpty)
                            {
                                break;
                            }
                        }

                        if (nextCol != columnIndex)
                        {
                            _cells[rowIndex, columnIndex].MoveUnit(_cells[rowIndex, nextCol]);

                            if (!_isMove)
                            {
                                _currentCountOfMoves--;
                                _isMove = true;
                            }
                        }
                    }
                }
            }
        }

        if (direction.x < 0) // влево
        {
            for (int rowIndex = 0; rowIndex < _size; rowIndex++) // идет свеху вниз
            {
                for (int columnIndex = 1; columnIndex < _size; columnIndex++) // 1 2 3
                {
                    if (!_cells[rowIndex, columnIndex].IsEmpty)
                    {
                        int nextCol = columnIndex;

                        for (int colDelta = columnIndex - 1; colDelta >= 0; colDelta--)
                        {
                            if (_cells[rowIndex, colDelta].IsEmpty)
                            {
                                nextCol = colDelta;
                            }
                            else if (_cells[rowIndex, colDelta].UnitLevel == _cells[rowIndex, columnIndex].UnitLevel && !_cells[rowIndex, colDelta].IsMerge)
                            {
                                nextCol = colDelta;
                                break;
                            }
                            else if (!_cells[rowIndex, colDelta].IsEmpty)
                            {
                                break;
                            }
                        }

                        if (nextCol != columnIndex)
                        {
                            _cells[rowIndex, columnIndex].MoveUnit(_cells[rowIndex, nextCol]);

                            if (!_isMove)
                            {
                                _currentCountOfMoves--;
                                _isMove = true;

                            }
                        }
                    }
                }
            }
        }

        if (direction.y > 0) // вверх
        {
            for (int rowIndex = 1; rowIndex < _size; rowIndex++) // идет свеху вниз 1 2 3
            {
                for (int columnIndex = 0; columnIndex < _size; columnIndex++) // 
                {
                    if (!_cells[rowIndex, columnIndex].IsEmpty)
                    {
                        int nextRow = rowIndex;

                        for (int rowDelta = rowIndex - 1; rowDelta >= 0; rowDelta--)
                        {
                            if (_cells[rowDelta, columnIndex].IsEmpty)
                            {
                                nextRow = rowDelta;
                            }
                            else if (_cells[rowDelta, columnIndex].UnitLevel == _cells[rowIndex, columnIndex].UnitLevel && !_cells[rowDelta, columnIndex].IsMerge)
                            {
                                nextRow = rowDelta;
                                break;
                            }
                            else if (!_cells[rowDelta, columnIndex].IsEmpty)
                            {
                                break;
                            }
                        }

                        if (nextRow != rowIndex)
                        {
                            _cells[rowIndex, columnIndex].MoveUnit(_cells[nextRow, columnIndex]);
                            if (!_isMove)
                            {
                                _currentCountOfMoves--;
                                _isMove = true;

                            }
                        }
                    }

                }

            }
        }

        if (direction.y < 0) // вниз
        {
            for (int rowIndex = _size - 2; rowIndex >= 0; rowIndex--) // идет сниху вверх 2 1 0
            {
                for (int columnIndex = 0; columnIndex < _size; columnIndex++) // 
                {
                    if (!_cells[rowIndex, columnIndex].IsEmpty)
                    {
                        int nextRow = rowIndex;

                        for (int rowDelta = rowIndex + 1; rowDelta < _size; rowDelta++)
                        {
                            if (_cells[rowDelta, columnIndex].IsEmpty)
                            {
                                nextRow = rowDelta;
                            }
                            else if (_cells[rowDelta, columnIndex].UnitLevel == _cells[rowIndex, columnIndex].UnitLevel && !_cells[rowDelta, columnIndex].IsMerge)
                            {
                                nextRow = rowDelta;
                                break;
                            }
                            else if (!_cells[rowDelta, columnIndex].IsEmpty)
                            {
                                break;
                            }
                        }

                        if (nextRow != rowIndex)
                        {
                            _cells[rowIndex, columnIndex].MoveUnit(_cells[nextRow, columnIndex]);

                            if (!_isMove)
                            {
                                _currentCountOfMoves--;
                                _isMove = true;
                            }
                        }
                    }
                }

            }
        }

        _isMove = false;
    }

    private void ResetCountOfMoves()
    {
        _currentCountOfMoves = _maxCountOfMoves;
    }

    private void OnCellMerge(Cell cell, int level)
    {
        _unitsSpawner.SpawnNextUnit(cell, level);
    }
}