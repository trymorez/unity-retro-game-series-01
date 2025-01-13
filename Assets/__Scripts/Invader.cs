using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class Invader : MonoBehaviour
{
    public int score;
    public Color color;
    public GameObject prefab;
    public MissilePool missilePool;
    public static Action<GameObject, GameObject> OnRecycleInvader;
    public static Action<int> OnInvaderDead;
    public Sprite[] sprite;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = color;
        rb = GetComponent<Rigidbody2D>();
        GameManager.InvaderMove += InvaderMove;
    }

    void InvaderMove(InvaderGroupInfo info, Vector3 move)
    {
        transform.Translate(move);
        spriteRenderer.sprite = sprite[info.sprite];
        float myPositionX = transform.position.x;
        float myPositionY = transform.position.y;
        RegisterShootPos();

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

    float rayLength = 3f;
    void RegisterShootPos()
    {
        if (GameManager.instance.invaderCanShoot)
        {
            Vector3 origin = transform.position + Vector3.down * 1.0f;
            
            RaycastHit2D ray = Physics2D.Raycast(origin, Vector2.down, rayLength);
            if (ray.collider == null || !ray.collider.CompareTag("Enemy"))
            {
                if (UnityEngine.Random.value > 0.5f)
                {
                    origin += Vector3.right * 0.1f;
                }
                else
                {
                    origin -= Vector3.right * 0.1f;
                }
                GameManager.instance.missileStartPos.Add(origin);
            }
            
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 origin = transform.position + Vector3.down * 1f;
        Gizmos.DrawLine(origin, origin + Vector3.down * rayLength);
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
        if (other.CompareTag("Bunker"))
        {
            Destroy(other.gameObject);
        }
    }
}
