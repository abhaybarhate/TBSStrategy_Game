using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cameraTransform;
    private bool invert = true;
    
    
    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (invert)
        {
            Vector3 direction = (cameraTransform.position - transform.position).normalized;
            transform.LookAt(transform.position + direction * -1);
        }
        else
        {
            transform.LookAt(cameraTransform);
        }
        
    }
}
