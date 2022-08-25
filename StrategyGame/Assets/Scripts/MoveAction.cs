using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{


    [SerializeField] private Animator UnitAnimator;  
    [SerializeField] int maxMoveDistance = 4;
    
    private Vector3 TargetPosition;
    private GridPosition gridPosition;
    private Unit unit;
    

    private void Awake() {
        unit = GetComponent<Unit>();
        TargetPosition = transform.position;
    }

    private void Start() {
        
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, TargetPosition) > .1f)
        {
            Vector3 MoveDirection = (TargetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += MoveDirection * Time.deltaTime * moveSpeed;
            float rotateSpeed = 10f;    
            transform.forward = Vector3.Lerp(transform.forward, MoveDirection, Time.deltaTime * rotateSpeed);
            UnitAnimator.SetBool("IsWalking", true);
        } 
        else {
            UnitAnimator.SetBool("IsWalking", false);
        }
        
    }

    public void Move(GridPosition gridPosition)
    {
        this.TargetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionsList = GetValidGridPositionList();
        return validGridPositionsList.Contains(gridPosition);
    }

    public List<GridPosition> GetValidGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();
        for(int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for(int z = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                GridPosition offsetGridPosition = new GridPosition(x,z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                if(unitGridPosition == testGridPosition) continue;
                if(LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;
                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

}
