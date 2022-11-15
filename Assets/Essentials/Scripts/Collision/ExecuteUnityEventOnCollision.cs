using UnityEngine;
using UnityEngine.Events;

public class ExecuteUnityEventOnCollision : ExecuteOnCollision
{
    public UnityEvent<Collider2D> OnColliderEnter;
    public UnityEvent<Collider2D> OnColliderExit;

    protected override void HandleCollisionEnter(Collider2D collider)
    {
        OnColliderEnter?.Invoke(collider);
    }

    protected override void HandleCollisionExit(Collider2D collider)
    {
        OnColliderExit?.Invoke(collider);
    }
}
