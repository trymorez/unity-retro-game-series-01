using System;
using Unity.VisualScripting;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public int score;
    public float color;
    public GameObject prefab;
    public static Action<GameObject, GameObject> OnRecycleInvader;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            OnRecycleInvader!.Invoke(gameObject, prefab);
        }
    }

}
