using UnityEngine;

abstract public class ExecuteOnCollision : MonoBehaviour
{
    [Header("References")]
    public CollisionDetector detector;

    protected virtual void Awake()
    {
        detector.OnEnter += HandleCollisionEnter;
        detector.OnExit += HandleCollisionExit;
    }

    abstract protected void HandleCollisionEnter(Collider2D collider);
    abstract protected void HandleCollisionExit(Collider2D collider);
}