using UnityEngine;

public class TimeController : MSingleton<TimeController>, IResettable
{
    private float slowMoTimer;
    private bool hasSlowed = false;

    public void DoSlowMotion(float factor, float duration)
    {
        Time.timeScale = factor;
        Time.fixedDeltaTime = factor * 0.02f;
        slowMoTimer = duration;
        hasSlowed = true;
    }

    public void PauseTime()
    {
        Time.timeScale = 0f;
    }

    public void ContinueTime()
    {
        ApplyReset();
    }

    private void Update()
    {
        if (hasSlowed)
        {
            slowMoTimer -= Time.unscaledDeltaTime;

            if (slowMoTimer <= 0.0f)
            {
                ApplyReset();
            }
        }
    }

    public void ApplyReset()
    {
        hasSlowed = false;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        slowMoTimer = 0.0f;
    }

}
