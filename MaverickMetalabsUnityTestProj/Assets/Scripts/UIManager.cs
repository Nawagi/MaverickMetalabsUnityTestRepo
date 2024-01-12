//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerTMP;
    [SerializeField] TextMeshProUGUI patricksSpawnedTMP;
    [SerializeField] TextMeshProUGUI roundTMP;
    [SerializeField] TextMeshProUGUI roundPatrickAmountTMP;
    [SerializeField] private float roundTime;

    RoundManager roundManager;

    private void Awake()
    {
        roundManager = FindObjectOfType<RoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (roundManager.roundState == RoundManager.RoundStateEnum.Spawning)
            DisplayTimer();
    }

    private void DisplayTimer()
    {
        roundTime += Time.deltaTime;

        string timer = "";

        //hours
        if (((int)roundTime / 3600) > 0)
            timer += Mathf.FloorToInt(((int)roundTime / 3600)).ToString("00") + ":";

        //minutes
        timer += Mathf.FloorToInt(((int)roundTime / 60) % 60).ToString("00") + ":";

        //seconds
        timer += Mathf.FloorToInt((roundTime % 60)).ToString("00");

        timerTMP.text = timer;
    }

    #region Subscribing to Events

    private void OnEnable()
    {
        EventManager.Instance.onPatrickSpawned += UpdatePatricksSpawnedDisplay;
        EventManager.Instance.onRoundEnd += ResetTimer;
        EventManager.Instance.onRoundStart += UpdateRoundPatrickAmount;
    }

    private void OnDisable()
    {
        EventManager.Instance.onPatrickSpawned -= UpdatePatricksSpawnedDisplay;
        EventManager.Instance.onRoundEnd -= ResetTimer;
        EventManager.Instance.onRoundStart -= UpdateRoundPatrickAmount;
    }

    void UpdatePatricksSpawnedDisplay(int patricksSpawned)
    {
        patricksSpawnedTMP.text = patricksSpawned.ToString();
    }

    void ResetTimer(int nextRound)
    {
        roundTMP.text = "Round " + nextRound;
        roundTMP.gameObject.SetActive(true);
        roundTime = 0;
    }

    void UpdateRoundPatrickAmount(int roundPatrickAmount)
    {
        roundPatrickAmountTMP.text = roundPatrickAmount.ToString();
        roundTMP.gameObject.SetActive(false);
    }

    #endregion Subscribing to Events
}
