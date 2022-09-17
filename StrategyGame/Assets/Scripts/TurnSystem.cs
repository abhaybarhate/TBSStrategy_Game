using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{

    public static TurnSystem Instance { get; private set;}
    public event EventHandler OnTurnChanges;

    private int TurnNumber;

    private void Awake() 
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one TurnSystem in Scene" + transform + "," + Instance);
            Destroy(gameObject);
            return;
        }    
        Instance = this;
    }

    public void NextTurn()
    {
        TurnNumber++;
        OnTurnChanges?.Invoke(this, EventArgs.Empty);
    }

    public int GetTurnNumber()
    {
        return TurnNumber;
    }

}
