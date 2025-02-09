using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float startTime = 60f;

    private float timeLeft;
    private WaitForSeconds waitTime;

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

    private IEnumerator StartTimer()
    {
        while (timeLeft > 0)
        {
            timeLeft -= 1f;
            OnTimeChanged?.Invoke(timeLeft);
            yield return waitTime;
        }
        
        OnTimerZero?.Invoke();
    }
}
