using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timerValue;

    private bool runTimer = false;

    public UnityEvent TimerEvent;

    public void StartTimer(float _timerValue)
    {
        timerValue = _timerValue;
        runTimer = true;
    }

    public void StopTimer()
    {
        runTimer = false;

    }

    private void Update()
    {
        if (runTimer)
        {
            timerValue -= Time.deltaTime;

            if (timerValue <= 0f)
            {
                StopTimer();

                if (TimerEvent != null)
                {
                    TimerEvent.Invoke();
                }
            }
        }
    }

}