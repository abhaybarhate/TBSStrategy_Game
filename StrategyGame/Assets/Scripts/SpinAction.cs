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
        }
    }

    public void Spin()
    {
        isSpinActionActive = true;
        totalSpinAmount = 0f;
    }
}
