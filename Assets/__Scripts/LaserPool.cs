using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPool : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;
    [SerializeField] int poolSize;
    Queue<GameObject> laserPool;

    void Start()
    {
        Laser.OnRecycleLaser += RecycleLaser;
        BunkerBlock.OnRecycleLaser += RecycleLaser;

        laserPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject laser = Instantiate(laserPrefab);
            laser.SetActive(false);
            laserPool.Enqueue(laser);
        }
    }

    void OnDisable()
    {
        Laser.OnRecycleLaser -= RecycleLaser;
        BunkerBlock.OnRecycleLaser -= RecycleLaser;
    }

    public GameObject GetLaser()
    {
        if (laserPool.Count > 0)
        {
            GameObject laser = laserPool.Dequeue();
            laser.SetActive(true);
            return laser;
        }
        else
        {
            GameObject laser = Instantiate(laserPrefab);
            return laser;
        }
    }

    public void RecycleLaser(GameObject laser)
    {
        laser.SetActive(false);
        laserPool.Enqueue(laser);
    }
}
