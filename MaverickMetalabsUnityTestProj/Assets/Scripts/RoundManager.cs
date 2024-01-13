using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public enum RoundStateEnum
    {
        Break,
        Spawning
    }

    public RoundStateEnum roundState;

    private int currentRound;
    public float breakSeconds;

    private int fibonacciTermOne = 0;
    private int fibonacciTermTwo = 1;
    private int currentFibonacciSequence;

    #region Subscribing to Events

    private void OnEnable()
    {
        EventManager.Instance.onLastPatrickOffScreen += EndRound;
    }

    private void OnDisable()
    {
        EventManager.Instance.onLastPatrickOffScreen -= EndRound;
    }

    public void EndRound()
    {
        currentRound++;
        roundState = RoundStateEnum.Break;
        EventManager.Instance.RoundEnd(currentRound);
        StartCoroutine(WaitToStartNextRound());
    }

    IEnumerator WaitToStartNextRound()
    {
        yield return new WaitForSeconds(breakSeconds);
        roundState = RoundStateEnum.Spawning;
        EventManager.Instance.RoundStart(GetCurrentFibonacciSequence());
    }

    int GetCurrentFibonacciSequence()
    {
        if (currentRound > 2)
        {
            fibonacciTermOne = fibonacciTermTwo;
            fibonacciTermTwo = currentFibonacciSequence;
            currentFibonacciSequence = fibonacciTermOne + fibonacciTermTwo;
            return currentFibonacciSequence;
        }

        return currentFibonacciSequence = 1;
    }

    #endregion Subscribing to Events


    #region For Test Purposes

    public int CheckFibonacciSequencePerRound()
    {
        currentRound++;
        return GetCurrentFibonacciSequence();
    }

    #endregion For Test Purposes
}