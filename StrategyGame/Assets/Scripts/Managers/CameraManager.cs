using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraManager : MonoBehaviour
{

    [SerializeField] private GameObject actionCameraGameObject;


    private void OnEnable()
    {
        ToggleActionCamera(false);
        BaseAction.OnAnyActionStart += BaseAction_OnAnyActionStarted;
        BaseAction.OnAnyActionCompleted += BaseAction_OnAnyActionCompleted;
    }
    
    private void OnDisable()
    {
        BaseAction.OnAnyActionStart -= BaseAction_OnAnyActionStarted;
        BaseAction.OnAnyActionCompleted -= BaseAction_OnAnyActionCompleted;
    }
    
    private void ToggleActionCamera(bool toggle)
    {
        actionCameraGameObject.SetActive(toggle);
    }

    private void BaseAction_OnAnyActionStarted(object sender, EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction :
                HandleCameraPositionForShootAction(shootAction);
                ToggleActionCamera(true);
                break;
            
        }
    }

    private void BaseAction_OnAnyActionCompleted(object sender, EventArgs e)
    {
        switch (sender)
        {
            case ShootAction shootAction :
                ToggleActionCamera(false);
                break;
            
        }
    }

    private void HandleCameraPositionForShootAction(ShootAction shootAction)
    {
        Unit shooterUnit = shootAction.GetUnit();
        Unit targetUnit = shootAction.GetTargetUnit();
        Vector3 direction = (targetUnit.GetWorldPosition() - shooterUnit.GetWorldPosition()).normalized;
        Vector3 cameraCharacterHeight = Vector3.up * 1.64f;
        float shoulderOffsetAmount = 0.4f;
        Vector3 shoulderOffset = Quaternion.Euler(0, 90f, 0f) * direction * shoulderOffsetAmount;
        Vector3 actionCameraPosition = shooterUnit.GetWorldPosition() + cameraCharacterHeight + shoulderOffset + direction * -1;
        actionCameraGameObject.transform.position = actionCameraPosition;
        actionCameraGameObject.transform.LookAt(targetUnit.GetWorldPosition() + cameraCharacterHeight);
    }
    
    
}
