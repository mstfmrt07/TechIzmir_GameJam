using UnityEngine;

public class Level : MonoBehaviour, IDisposable
{
    public Boss boss;

    public void Dispose()
    {
        Destroy(gameObject);
    }
}
