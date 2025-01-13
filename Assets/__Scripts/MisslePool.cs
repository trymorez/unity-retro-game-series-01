using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePool : MonoBehaviour
{
    [SerializeField] GameObject missilePrefab;
    [SerializeField] int poolSize;
    Queue<GameObject> missilePool;
    public static MissilePool Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        MissilePoolCreate();
    }

    private void MissilePoolCreate()
    {
        Missile.OnRecycleMissle += MissleRecycle;

        missilePool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject missile = Instantiate(missilePrefab);
            missile.SetActive(false);
            missilePool.Enqueue(missile);
        }
    }

    public void MissilePoolInit()
    {
        var activeItems = new List<GameObject>();

        foreach (var missile in missilePool)
        {
            activeItems.Add(missile);
        }

        foreach (var missile in activeItems)
        {
            MissleRecycle(missile);
        }
    }

    void OnDestroy()
    {
        Missile.OnRecycleMissle -= MissleRecycle;
    }

    public GameObject MissileGet()
    {
        if (missilePool.Count > 0)
        {
            GameObject missile = missilePool.Dequeue();
            missile.SetActive(true);
            return missile;
        }
        else
        {
            GameObject missle = Instantiate(missilePrefab);
            return missle;
        }
    }

    public void MissleRecycle(GameObject missile)
    {
        if (missile.activeSelf == true)
        {
            missile.SetActive(false);
            missilePool.Enqueue(missile);
        }
    }
}
