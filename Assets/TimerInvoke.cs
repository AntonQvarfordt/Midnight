using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerInvoke : MonoBehaviour {

    [SerializeField]
    private float _timerDurationFull;
    [SerializeField]
    private float _timeLeft;
    [SerializeField]
    private UnityAction[] _actions;

    public UnityAction SetTimedInvoke (float rangeA, float rangeB, UnityAction[] actions)
    {
        _timerDurationFull = Random.Range(rangeA, rangeB);
        _timeLeft = _timerDurationFull;
        _actions = actions;
        StartCoroutine(CountdownTimer((int)_timerDurationFull, 1));

        var returnAction = new UnityAction(CancelTimer);
        return returnAction;

    }

    private IEnumerator CountdownTimer (int startTime, float updateFrequencySeconds)
    {
        var timePool = startTime;
        while (timePool > 0)
        {
            timePool--;
            yield return new WaitForSeconds(updateFrequencySeconds);
        }

        foreach (UnityAction action in _actions)
        {
            action.Invoke();
        }
    }

    private void CancelTimer ()
    {
        StopCoroutine("CountdownTimer");
    }
}
