using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionDetector : Activatable
{
    public LayerMask targetLayer;
    public bool deactivateOnDetect;
    public bool deactivateOnExit;

    public Action<Collider2D> OnEnter;
    public Action<Collider2D> OnExit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsActive || !targetLayer.Includes(other.gameObject.layer))
            return;

        OnEnter?.Invoke(other);

        if (deactivateOnDetect)
            IsActive = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IsActive || !targetLayer.Includes(other.gameObject.layer))
            return;

        OnExit?.Invoke(other);

        if (deactivateOnExit)
            IsActive = false;
    }

    protected override void Tick()
    {
    }
}
