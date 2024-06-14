using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float targetTime = 60.0f;
    public bool hasStarted = false;

    public UnityEvent timerFinished;

    private void Update()
    {
        if (hasStarted)
        {
            RunTimer();
        }
    }

    public void StartTimer(float pTime)
    {
        if (!hasStarted)
        {
            hasStarted = true;
            targetTime = pTime;
        }
    }

    private void RunTimer()
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
    }

    void timerEnded()
    {
        timerFinished?.Invoke();
        hasStarted = false;
    }


}
