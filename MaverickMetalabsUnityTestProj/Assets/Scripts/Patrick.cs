//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Patrick : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float speed;

    private IObjectPool<Patrick> patrickPool;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    public void SetPool(IObjectPool<Patrick> pool)
    {
        patrickPool = pool;
    }

    private void OnBecameInvisible()
    {
        if (transform.position.x > 0)
        {
            EventManager.Instance.PatrickOffScreen();
            Invoke("ReleasePool", 1f);
        }
    }

    void ReleasePool()
    {
        patrickPool.Release(this);
    }
}