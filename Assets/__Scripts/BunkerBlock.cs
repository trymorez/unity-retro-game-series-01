using System;
using UnityEngine;

public class BunkerBlock : MonoBehaviour
{
    public static Action<GameObject> OnRecycleLaser;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");
        if (other.CompareTag("Laser"))
        {
            Destroy(gameObject);
            OnRecycleLaser!.Invoke(other.gameObject);
        }
    }
}
