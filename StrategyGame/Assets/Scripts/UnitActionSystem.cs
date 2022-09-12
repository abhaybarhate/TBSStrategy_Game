using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }       // Singleton Pattern Instance

    public event EventHandler OnSelectedUnitChanged;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask UnitLayerMask;

    private void Awake() 
    {
        if(Instance != null)
        {
            Debug.LogError("There's more than one UnitActionSystem !" + transform + "-"+ Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update() 
    {   
        if(Input.GetMouseButtonDown(0))
        {
            if(TryHandleUnitSelection()) return;
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetMousePosition());
            Debug.Log(mouseGridPosition);
            if(selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                Debug.Log("I m going to move");
                selectedUnit.GetMoveAction().Move(mouseGridPosition);
                
            }
            
        }
        if(Input.GetMouseButtonDown(1))
        {
            selectedUnit.GetSpinAction().Spin();
        }
    }

    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, UnitLayerMask))
        {
            if(hitInfo.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        // if(OnSelectedUnitChanged != null)
        // {
        //     OnSelectedUnitChanged(this, EventArgs.Empty);
        // }
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

}
