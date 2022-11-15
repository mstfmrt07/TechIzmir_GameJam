using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TapDetector : Activatable, IPointerDownHandler, IPointerUpHandler
{
    public Action OnTap;
    public Action OnRelease;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsActive)
            OnTap?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsActive)
            OnRelease?.Invoke();
    }

    protected override void Tick()
    {
    }
}
