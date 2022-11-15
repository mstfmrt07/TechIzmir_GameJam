using UnityEngine;

public class FPS_Stabilizer : MonoBehaviour
{
    public int targetFrameRate;

    private void Awake()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}
