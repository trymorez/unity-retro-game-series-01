using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class InvaderPool : MonoBehaviour
{
    [SerializeField] MissilePool missilePool;
    public InvaderData[] invaderDatas;
    public int[] poolSize;
    public static InvaderPool instance;
    
    Dictionary<GameObject, Queue<GameObject>> invaderPools = new Dictionary<GameObject, Queue<GameObject>>();

    void Awake()
    {
        instance = this;
        Invader.OnRecycleInvader += RecycleInvader;

        invaderPoolInitialize();
    }

    void invaderPoolInitialize()
    {
        int typeIndex = 0;

        foreach (var invaderData in invaderDatas)
        {
            Queue<GameObject> invaderQueue = new Queue<GameObject>();
            for (int i = 0; i < poolSize[typeIndex]; i++)
            {
                invaderPoolPopulate(invaderQueue, invaderData);
            }
            invaderPools.Add(invaderData.prefab, invaderQueue);
            typeIndex++;
        }
    }

    void invaderPoolPopulate(Queue<GameObject> invaderQueue, InvaderData invaderData)
    {
        GameObject invaderInstance = Instantiate(invaderData.prefab);
        Invader invader = invaderInstance.GetComponent<Invader>();
        invader.score = invaderData.score;
        invader.color = invaderData.color;
        invader.prefab = invaderData.prefab;
        invader.missilePool = missilePool;
        invaderInstance.SetActive(false);
        invaderQueue.Enqueue(invaderInstance);
    }

    void OnDisable()
    {
        Invader.OnRecycleInvader -= RecycleInvader;
    }

    public GameObject GetInvader(GameObject prefab)
    {
        if (invaderPools.ContainsKey(prefab) && invaderPools[prefab].Count > 0)
        {
            GameObject invader = invaderPools[prefab].Dequeue();
            invader.SetActive(true);
            return invader;
        }
        return null;
    }

    public void RecycleInvader(GameObject invader, GameObject prefab)
    {
        invader.SetActive(false);
        invaderPools[prefab].Enqueue(invader);
    }
}
