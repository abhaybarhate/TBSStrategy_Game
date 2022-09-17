using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] Button endTurnButton;
    [SerializeField] TextMeshProUGUI TurnNumberText;

    private void Start() {
        endTurnButton.onClick.AddListener(()=> {
            TurnSystem.Instance.NextTurn();
        });
        UpdateTurnNumberText();
        TurnSystem.Instance.OnTurnChanges += TurnSystem_OnTurnChanges;
    }

    private void TurnSystem_OnTurnChanges(object sender, EventArgs e)
    {
        UpdateTurnNumberText();
    }

    public void UpdateTurnNumberText()
    {
        int turnNumber = TurnSystem.Instance.GetTurnNumber();
        TurnNumberText.text = "TURN " + turnNumber;
    }
}
