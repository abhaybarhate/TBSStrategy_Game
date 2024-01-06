using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthMax = 100f;
    public event EventHandler OnUnitDie;
    public event EventHandler OnUnitDamage;
    
    private float health;

    private void Start()
    {
        health = healthMax;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        OnUnitDamage?.Invoke(this, EventArgs.Empty);
        if (health <= 0)
        {
            health = 0;
            Die();
            Debug.Log($"Health : {health}");
        }
        
    }

    private void Die()
    {
        OnUnitDie?.Invoke(this,EventArgs.Empty);
    }

    public float GetHealthNormalized()
    {
        return health / healthMax;
    }
    
}
