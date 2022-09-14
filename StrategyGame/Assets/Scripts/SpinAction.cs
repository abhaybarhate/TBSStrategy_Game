using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{

    

    private float totalSpinAmount;


    void Update()
    {
        if(!isSpinActionActive) return;
        
        float spinAddAmount = 360f * Time.deltaTime;
        
        transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        totalSpinAmount += spinAddAmount;
        if(totalSpinAmount >= 360f)
        {
            isSpinActionActive = false;
            onActionComplete();
        }
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
        isSpinActionActive = true;
        totalSpinAmount = 0f;
    }

    public override List<GridPosition> GetValidGridPositionsList()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();
        return new List<GridPosition>{ unitGridPosition };
    }

    public override string GetActionName()
    {
        return "Spin";
    }

}
