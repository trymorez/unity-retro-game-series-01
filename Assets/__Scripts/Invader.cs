using System;
using Unity.VisualScripting;
using UnityEngine;


public class Invader : MonoBehaviour
{
    public int score;
    public Color color;
    public GameObject prefab;
    public static Action<GameObject, GameObject> OnRecycleInvader;
    public static Action<int> OnInvaderDead;
    public Sprite[] sprite;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = color;
        GameManager.InvaderMove += InvaderMove;
    }

    void InvaderMove(InvaderGroupInfo info, Vector3 move)
    {
        transform.Translate(move);
        spriteRenderer.sprite = sprite[info.sprite];
        float myPositionX = transform.position.x;
        float myPositionY = transform.position.y;

        if (move.x > 0)
        {
            if (myPositionX > info.rightX)
            {
                info.rightX = myPositionX;
            }
            if (myPositionX > info.leftX)
            {
                info.leftX = myPositionX;
            }
        }
        if (move.x < 0)
        {
            if (myPositionX < info.rightX)
            {
                info.rightX = myPositionX;
            }
            if (myPositionX < info.leftX)
            {
                info.leftX = myPositionX;
            }
        }
        if (myPositionY < info.bottomY)
        {
            info.bottomY = myPositionY;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            SoundManager.Play("InvaderDestroied");
            GameManager.InvaderMove -= InvaderMove;
            OnInvaderDead!.Invoke(score);
            OnRecycleInvader!.Invoke(gameObject, prefab);
        }
    }





}
