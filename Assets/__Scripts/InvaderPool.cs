using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class InvaderPool : MonoBehaviour
{
    public InvaderData[] invaderDatas;
    public int[] poolSize;
    public static InvaderPool instance;
    
    Dictionary<GameObject, Queue<GameObject>> invaderPools = new Dictionary<GameObject, Queue<GameObject>>();

    void Awake()
    {
        instance = this;
        Invader.OnRecycleInvader += RecycleInvader;

        int typeIndex = 0;

        foreach (var invaderData in invaderDatas)
        {
            Queue<GameObject> invaderQueue = new Queue<GameObject>();
            for (int i = 0; i < poolSize[typeIndex]; i++)
            {
                GameObject invader = Instantiate(invaderData.prefab);
                Invader invaderObj = invader.GetComponent<Invader>();
                invaderObj.prefab = invaderData.prefab;
                invader.SetActive(false);
                invaderQueue.Enqueue(invader);
            }
            invaderPools.Add(invaderData.prefab, invaderQueue);
            typeIndex++;
        }
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
