using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePool : MonoBehaviour
{
    [SerializeField] GameObject missilePrefab;
    [SerializeField] int poolSize;
    Queue<GameObject> missilePool;

    void Start()
    {
        Missile.OnRecycleMissle += RecycleMissle;

        missilePool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject missile = Instantiate(missilePrefab);
            missile.SetActive(false);
            missilePool.Enqueue(missile);
        }
    }

    void OnDestroy()
    {
        Missile.OnRecycleMissle -= RecycleMissle;
    }

    public GameObject GetMissile()
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

    public void RecycleMissle(GameObject missile)
    {
        if (missile.activeSelf == true)
        {
            missile.SetActive(false);
            missilePool.Enqueue(missile);
        }
    }
}
