using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private EnemyState state;
    private float timer;


    private void Awake()
    {
        state = EnemyState.WaitingForEnemyTurn;
    }

    private void Start() 
    {
        TurnSystem.Instance.OnTurnChanges += TurnSystem_OnTurnChanges;    
    }

    private void Update() 
    {
        if(TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }

        switch (state)
        {
            case EnemyState.WaitingForEnemyTurn :
                break;
            case EnemyState.TakingTurn :
                timer -= Time.deltaTime;
                if(timer <= 0f)
                {
                    state = EnemyState.Busy;
                    if (TryTakeEnemyAction(SetStateTakingTurn))
                    {
                        state = EnemyState.Busy;
                    }
                    else
                    {
                        // No more enemies have actions to take
                        TurnSystem.Instance.NextTurn();
                    }
                }
                break;
            case EnemyState.Busy :
                break;
            default:
                break;
                
        }

    }

    private void SetStateTakingTurn()
    {
        timer = 0.5f;
        state = EnemyState.TakingTurn;
    }
    
    private void TurnSystem_OnTurnChanges(object sender, EventArgs e)
    {
        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            state = EnemyState.TakingTurn;
            timer = 3f;
        }
        
    }
    
    private bool TryTakeEnemyAction(Action onEnemyAIActionComplete)
    {
        foreach (Unit enemyUnit in UnitManager.Instance.GetEnemyUnitList())
        {
            if(TryTakeEnemyAction(enemyUnit, onEnemyAIActionComplete))
                return true;
        }

        return false;
    }

    private bool TryTakeEnemyAction(Unit enemy, Action onEnemyAIActionComplete)
    {
        
        EnemyAIAction bestEnemyAIAction = null;
        BaseAction bestBaseAction = null;
        foreach (BaseAction baseAction in enemy.GetBaseActionArray())
        {

            if (!enemy.CanSpendActionPoints(baseAction))
            {
                // Enemy cannot afford this action
                continue;
            }

            if (bestEnemyAIAction == null)
            {
                bestEnemyAIAction = baseAction.GetBestEnemyAIAction();
                bestBaseAction = baseAction;
            }
            else
            {
                EnemyAIAction testEnemyAIAction = baseAction.GetBestEnemyAIAction();
                if (testEnemyAIAction != null && testEnemyAIAction.actionValue > bestEnemyAIAction.actionValue)
                {
                    bestEnemyAIAction = testEnemyAIAction;
                    bestBaseAction = baseAction;
                }
            }
            
        }

        if (bestEnemyAIAction != null && enemy.TrySpendActionPointsToTakeAction(bestBaseAction))
        {
            bestBaseAction.TakeAction(bestEnemyAIAction.gridPosition, onEnemyAIActionComplete);
            return true;
        }
        else
        {
            return false;
        }
        
        
    }
    
    
    private enum EnemyState
    {
        WaitingForEnemyTurn,
        TakingTurn,
        Busy
    }
    
}
