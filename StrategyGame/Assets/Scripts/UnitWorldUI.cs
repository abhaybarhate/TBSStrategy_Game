using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI actionPointsText;
    [SerializeField] private Unit unit;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private HealthSystem healthSystem;

    private void OnEnable()
    {
        healthSystem.OnUnitDamage += Unit_OnUnitDamage;
        Unit.OnAnyActionPointsChange += Unit_OnAnyActionPointsChange;
    }
    
    private void Start()
    {
        
        UpdateActionPointsText();
    }

    private void OnDisable()
    {
        healthSystem.OnUnitDamage -= Unit_OnUnitDamage;
        Unit.OnAnyActionPointsChange -= Unit_OnAnyActionPointsChange;
    }

    private void Unit_OnUnitDamage(object sender, EventArgs e)
    {
        UpdateHealthBar();
    }

    private void UpdateActionPointsText()
    {
        Debug.Log("Updating the Action Points text" + unit.GetActionPoints());
        actionPointsText.text = unit.GetActionPoints().ToString();
    }
    
    private void Unit_OnAnyActionPointsChange(object sender, EventArgs e)
    {
        UpdateActionPointsText();
    }

    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = healthSystem.GetHealthNormalized();
    }
    
}
