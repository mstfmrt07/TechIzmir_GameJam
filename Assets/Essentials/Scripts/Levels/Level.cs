using UnityEngine;

public class Level : MonoBehaviour, IDisposable
{
    public void Dispose()
    {
        Destroy(gameObject);
    }
}
