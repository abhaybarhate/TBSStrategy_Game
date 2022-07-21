using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator UnitAnimator;  
    private Vector3 TargetPosition;
    private GridPosition gridPosition;

    private void Awake() {
        TargetPosition = transform.position; 
    }

    // Start is called before the first frame update
    void Start()
    {
        if(LevelGrid.Instance) {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
        }
    }

    // Update is called once per frame
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
        if(LevelGrid.Instance)
        {
            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if(newGridPosition != gridPosition)
            {
                LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
                gridPosition = newGridPosition;
            }
        }
    }

    public void Move(Vector3 TargetPosition)
    {
        this.TargetPosition = TargetPosition;
    }


}
