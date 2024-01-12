//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Patrick patrickPrefab;
    private IObjectPool<Patrick> patrickPool;

    [SerializeField] private float yPosOffset = 2.5f;
    [SerializeField] private int spawnedAmount;
    [SerializeField] private int amountToSpawn;
    [SerializeField] private float spawnIntervalSeconds = .2f;

    private List<Patrick> roundPatrickList;

    private void Awake()
    {
        patrickPool = new ObjectPool<Patrick>(
            CreatePatrick,
            OnGet,
            OnRelease
            );

        roundPatrickList = new List<Patrick>();

        transform.position = new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x - 1, 
                                         transform.position.y, transform.position.z);
    }

    private Patrick CreatePatrick()
    {
        Patrick patrick = Instantiate(patrickPrefab);
        patrick.SetPool(patrickPool);
        return patrick;
    }

    private void OnGet(Patrick patrick)
    {
        float randomYPos = Random.Range(transform.position.y - yPosOffset, transform.position.y + yPosOffset);
        patrick.transform.position = new Vector3(transform.position.x, randomYPos, transform.position.z);
        roundPatrickList.Add(patrick);
        patrick.gameObject.SetActive(true);
    }

    private void OnRelease(Patrick patrick)
    {
        patrick.gameObject.SetActive(false);
    }

    #region Subscribing to Events

    private void OnEnable()
    {
        EventManager.Instance.onRoundStart += StartSpawning;
        EventManager.Instance.onPatrickOffScreen += RemoveItemFromRoundPatrickList;
    }

    private void OnDisable()
    {
        EventManager.Instance.onRoundStart -= StartSpawning;
        EventManager.Instance.onPatrickOffScreen -= RemoveItemFromRoundPatrickList;
    }

    void StartSpawning(int patricksToSpawn)
    {
        amountToSpawn += patricksToSpawn;
        SpawnPatrick();
    }

    void SpawnPatrick()
    {
        patrickPool.Get();
        spawnedAmount++;
        EventManager.Instance.PatrickSpawned(spawnedAmount);

        if (spawnedAmount < amountToSpawn)
            Invoke("SpawnPatrick", spawnIntervalSeconds);
    }

    void RemoveItemFromRoundPatrickList()
    {
        if (roundPatrickList.Count > 0)
            roundPatrickList.RemoveAt(0);

        if (roundPatrickList.Count == 0)
            EventManager.Instance.LastPatrickOffScreen();
    }

    #endregion Subscribing to Events
}