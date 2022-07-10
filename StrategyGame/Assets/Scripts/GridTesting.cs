using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTesting : MonoBehaviour
{
    [SerializeField] private Transform gridDebugObjectTransform;
    private GridSystem gridSystem;
    private void Start() 
    {
        gridSystem = new GridSystem(10, 10, 2f);   
        gridSystem.CreateDebugObjects(gridDebugObjectTransform);
        Debug.Log(new GridPosition(5,7)); 
    }

}
