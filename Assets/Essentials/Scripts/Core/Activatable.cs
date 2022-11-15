using UnityEngine;

public abstract class Activatable : MonoBehaviour
{
    [Header("Values")]
    public bool activateAtAwake = false;
    private bool isActive = false;
    public bool IsActive
    {
        get => isActive;
        set
        {
            isActive = value;

            if (isActive)
                OnActivate();
            else
                OnDeactivate();
        }
    }

    protected virtual void Awake()
    {
        isActive = activateAtAwake;
        enabled = activateAtAwake;
    }

    private void Update()
    {
        Tick();
    }

    protected virtual void OnActivate()
    {
        enabled = true;
    }

    protected virtual void OnDeactivate()
    {
        enabled = false;
    }

    abstract protected void Tick();
}
