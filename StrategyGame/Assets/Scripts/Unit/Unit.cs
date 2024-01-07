using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator UnitAnimator;  
    [SerializeField] private bool isEnemy;
    private const int ACTION_POINTS_MAX = 2;
    private Vector3 TargetPosition;
    private GridPosition gridPosition;
    private HealthSystem healthSystem;
    private MoveAction moveAction;
    private SpinAction spinAction;
    private ShootAction shootAction;
    private BaseAction[] baseActionArray;
    private int unitActionPoints = ACTION_POINTS_MAX;


    public static event EventHandler OnAnyActionPointsChange;
    public static event EventHandler OnAnyUnitSpawned;
    public static event EventHandler OnAnyUnitDead;
    
    
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        shootAction = GetComponent<ShootAction>();
        baseActionArray = GetComponents<BaseAction>();
    }

    private void OnEnable()
    {
        healthSystem.OnUnitDie += HealthSystem_OnUnitDie;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(LevelGrid.Instance) {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
        }

        TurnSystem.Instance.OnTurnChanges += TurnSystem_OnTurnChanges;
        OnAnyUnitSpawned?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        healthSystem.OnUnitDie -= HealthSystem_OnUnitDie;
    }

    // Update is called once per frame
    void Update()
    {

        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            // Unit changed Grid Position
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }

    }

    #region PublicGetMethods;
    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public SpinAction GetSpinAction()
    {
        return spinAction;
    }

    public ShootAction GetShootAction()
    {
        return shootAction;
    }
    

    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }

    public BaseAction[] GetBaseActionArray()
    {
        return baseActionArray;
    }
    
    public int GetActionPoints()
    {
        return unitActionPoints;
    }

    public bool IsEnemy()
    {
        return isEnemy;
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    public float GetHealthNormalized()
    {
        return healthSystem.GetHealthNormalized();
    }
    
    #endregion
    
    #region PublicMethods
    public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
    {
        if(CanSpendActionPoints(baseAction))
        {
            SpendActionPoints(baseAction.GetActionPointsCost());
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanSpendActionPoints(BaseAction baseAction)
    {
        if(unitActionPoints >= baseAction.GetActionPointsCost())
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public void Damage(float damageAmount)
    {
        healthSystem.TakeDamage(damageAmount);
    }

    #endregion
    
    #region PrivateMethods
    private void TurnSystem_OnTurnChanges(object sender, EventArgs e)
    {
        if((IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) || (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn()))
        {
            unitActionPoints = ACTION_POINTS_MAX;
            OnAnyActionPointsChange?.Invoke(this, EventArgs.Empty);
        }    
    }

    private void HealthSystem_OnUnitDie(object sender, EventArgs e)
    {
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);
        OnAnyUnitDead?.Invoke(this, EventArgs.Empty);
        Destroy(this.gameObject);
    }
    
    private void SpendActionPoints(int amount)
    {
        unitActionPoints -= amount;
        OnAnyActionPointsChange?.Invoke(this, EventArgs.Empty);
    }
    
    #endregion
}
