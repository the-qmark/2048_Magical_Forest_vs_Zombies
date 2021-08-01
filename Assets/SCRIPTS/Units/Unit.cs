using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [SerializeField] private int _level;
    

    public int Level { get => _level; private set => _level = value; }
    public bool IsMoving;

    public event UnityAction MoveStart;
    public event UnityAction MoveEnd;
    public event UnityAction<Unit> Die;

    private Cell _parentCell;
    private int _speed;

    void Start()
    {
        _speed = 6;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving)
        {
            if (transform.position == _parentCell.transform.position)
            {
                Stop();
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, _parentCell.transform.position, Time.deltaTime * _speed);
        }
    }

    private void Stop()
    {
        //Debug.Log("STOP");
        //Die?.Invoke(this);
        IsMoving = false;
        MoveEnd?.Invoke();
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void OnDestroy()
    {
        Die?.Invoke(this);
    }

    public void SetParentCell(Cell cell)
    {
        _parentCell = cell;
    }

    public void MoveTo(Cell cellToMove)
    {
        Vector3 newDir = cellToMove.transform.position - transform.position;

        transform.rotation = Quaternion.LookRotation(newDir);

        SetParentCell(cellToMove);

        IsMoving = true;
        //Debug.Log("MOVE");

        MoveStart?.Invoke();
    }
}
