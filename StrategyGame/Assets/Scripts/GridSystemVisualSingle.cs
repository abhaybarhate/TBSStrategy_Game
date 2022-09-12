using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] Color HiddenRenderingColorInner;
    [SerializeField] Color HiddenRenderingColorOuter;
    [SerializeField] Color ShowRenderingColorInner;
    [SerializeField] Color ShowRenderingColorOuter;
    [SerializeField] SpriteRenderer InnerSquareRenderer;
    [SerializeField] SpriteRenderer OuterSquareRenderer;

    void Start()
    {
        
    }

    public void Show()
    {
        InnerSquareRenderer.enabled = true;
        OuterSquareRenderer.enabled = true;
    }

    public void Hide()
    {
        InnerSquareRenderer.enabled = false;
        OuterSquareRenderer.enabled = false;
    }

}
