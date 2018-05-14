using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Button3D : MonoBehaviour 
{
    public UnityEvent function;
    private MeshRenderer msRenderer;
    private Color originalColor;
    public bool IsClicked { get; private set; }
    public bool CanClick { get; set; }

    private void Start()
    {
        msRenderer = GetComponentInChildren<MeshRenderer>() ?? GetComponentInParent<MeshRenderer>();
        CanClick = true;
        originalColor = msRenderer.material.color;
    }

    public void OnClick()
    {
        if (!CanClick)
            return;

        ToBlack(0.5f);
        function.Invoke();
        IsClicked = true;
    }

    public void OnRelease()
    {
        if (!CanClick)
            return;

        ToBlack(0.2f);
    }

    public void OnEnter()
    {
        if (!CanClick)
            return;

        ToBlack(0.2f);
    }

    public void OnExit()
    {
        if (!CanClick)
            return;

        IsClicked = false;
        ResetColor();
    }

    private void ResetColor()
    {
        msRenderer.material.color = originalColor;
    }

    private void ToBlack(float percentage)
    {
        Color newColor = Color.Lerp(originalColor,Color.black, percentage);
        msRenderer.material.color = newColor;
    }

}


