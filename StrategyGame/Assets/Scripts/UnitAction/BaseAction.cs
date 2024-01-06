using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{

    [SerializeField] public int actionCost;
    
    public static event EventHandler OnAnyActionStart ;
    public static event EventHandler OnAnyActionCompleted;
    
    protected bool isActive;
    
    protected Unit unit;
    protected Action onActionComplete;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public abstract string GetActionName();

    public abstract void TakeAction(GridPosition gridPosition, Action onActionComplete);
    
    public virtual bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionsList = GetValidGridPositionsList();
        return validGridPositionsList.Contains(gridPosition);
    }

    public abstract List<GridPosition> GetValidGridPositionsList();

    public virtual int GetActionPointsCost()
    {
        return 1;
    }

    protected void ActionStart(Action onActionComplete)
    {
        isActive = true;
        this.onActionComplete = onActionComplete;
        OnAnyActionStart?.Invoke(this, EventArgs.Empty);
    }

    protected void ActionComplete()
    {
        isActive = false;
        onActionComplete();
        OnAnyActionCompleted?.Invoke(this, EventArgs.Empty);
    }

    public Unit GetUnit()
    {
        return unit;
    }

    public EnemyAIAction GetBestEnemyAIAction()
    {
        List<EnemyAIAction> enemyAiActionList = new List<EnemyAIAction>();
        List<GridPosition> validActionGridPositionsList = GetValidGridPositionsList();
        foreach (GridPosition position in validActionGridPositionsList)
        {
            EnemyAIAction enemyAIAction = GetEnemyAIAction(position);
            enemyAiActionList.Add(enemyAIAction);
        }

        if (enemyAiActionList.Count > 0)
        {
            enemyAiActionList.Sort((EnemyAIAction a, EnemyAIAction b) => b.actionValue - a.actionValue);
            return enemyAiActionList[0];
        }
        else
        {
            // No Possible Actions
            return null;
        }
        
    }

    public abstract EnemyAIAction GetEnemyAIAction(GridPosition position);


}
