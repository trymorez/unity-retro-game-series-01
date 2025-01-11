using System;
using Unity.VisualScripting;
using UnityEngine;


public class Invader : MonoBehaviour
{
    public int score;
    public Color color;
    public GameObject prefab;
    public static Action<GameObject, GameObject> OnRecycleInvader;
    float screenEdge = 15f;

    SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = color;
        GameManager.InvaderMove += InvaderMove;
    }

    void InvaderMove(InvaderGroupBound bound, Vector3 move)
    {
        transform.Translate(move);
        float myPositionX = transform.position.x;
        float myPositionY = transform.position.y;

        if (move.x > 0)
        {
            if (myPositionX > bound.rightX)
            {
                bound.rightX = myPositionX;
            }
            if (myPositionX > bound.leftX)
            {
                bound.leftX = myPositionX;
            }
        }

        if (move.x < 0)
        {
            if (myPositionX < bound.rightX)
            {
                bound.rightX = myPositionX;
            }
            if (myPositionX < bound.leftX)
            {
                bound.leftX = myPositionX;
            }
        }


        if (myPositionY > bound.topY)
        {
            bound.topY = myPositionY;
        }
        if (myPositionY < bound.bottomY)
        {
            bound.bottomY = myPositionY;
        }
    }





    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            OnRecycleInvader!.Invoke(gameObject, prefab);
        }
    }





}
