using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private int _size;
    [SerializeField] private float _space;
    [SerializeField] private Cell _cellPrefab;

    private Cell[,] _cells;

    void Start()
    { 

        GenerateCells();
    }

    
    void Update()
    {
        
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
		        posX += sizeOfCell + _space;
	        }
            posX = startPosX;
            posZ -= (sizeOfCell + _space);
        }
    }
}