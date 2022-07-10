using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{

    private GridSystem gridSystem;
    private GridPosition gridPosition;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridPosition = gridPosition;
        this.gridSystem = gridSystem;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }

}
