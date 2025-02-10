using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float startTime = 60f;

    private float timeLeft;
    private WaitForSeconds waitTime;

    private bool _timerActive = true;

    public UnityEvent<float> OnTimeChanged;
    public UnityEvent OnTimerZero;

    private void Awake()
    {
        timeLeft = startTime;
        waitTime = new WaitForSeconds(1f);
    }

    public void StartCountdown()
    {
        StartCoroutine(StartTimer());
    }

    public void StopCountdown() => _timerActive = false;

    private IEnumerator StartTimer()
    {
        while (timeLeft > 0 && _timerActive)
        {
            timeLeft -= 1f;
            OnTimeChanged?.Invoke(timeLeft);
            yield return waitTime;
        }
        
        OnTimerZero?.Invoke();
    }
}
