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

        laserPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject laser = Instantiate(laserPrefab);
            laser.SetActive(false);
            laserPool.Enqueue(laser);
        }
    }

    void OnDestroy()
    {
        Laser.OnRecycleLaser -= RecycleLaser;
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
        //if (laser.transform.position.y < 10)
        //{
        //    Debug.Log("gotcha!");
        //    Debug.Log(laser.transform.position.y);
        //}
        if (laser.activeSelf == false)
        {
            Debug.Log("somethings wrong");
        }
        laser.SetActive(false);
        laserPool.Enqueue(laser);
    }
}
