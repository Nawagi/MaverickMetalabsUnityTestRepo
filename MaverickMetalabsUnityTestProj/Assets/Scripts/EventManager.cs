//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public event Action onPatrickOffScreen;

    public void PatrickOffScreen()
    {
        onPatrickOffScreen?.Invoke();
    }

    public event Action onLastPatrickOffScreen;

    public void LastPatrickOffScreen()
    {
        onLastPatrickOffScreen?.Invoke();
    }

    public event Action<int> onRoundEnd;

    public void RoundEnd(int nextRound)
    {
        onRoundEnd?.Invoke(nextRound);
    }

    public event Action<int> onRoundStart;

    public void RoundStart(int patricksToSpawn)
    {
        onRoundStart?.Invoke(patricksToSpawn);
    }

    public event Action<int> onPatrickSpawned;

    public void PatrickSpawned(int patricksSpawned)
    {
        onPatrickSpawned?.Invoke(patricksSpawned);
    }
}