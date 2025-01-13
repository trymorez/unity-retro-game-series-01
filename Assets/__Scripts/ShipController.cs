using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class ShipController : MonoBehaviour
{
    float moveX;
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] LaserPool LaserPool;
    [SerializeField] float laserDelay = 0.5f;
    float nextLaserTime;
    Animator animator;
    public static Action OnShipDestoried;
    bool isShipDestroied;
    float shipWidth;
    Vector2 screenBounds;
    float posY;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        shipWidth = GetComponent<Collider2D>().bounds.extents.x;
        posY = transform.position.y;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    void Update()
    {
        ShipMove();
    }

    private void ShipMove()
    {
        if (isShipDestroied)
        {
            rb.linearVelocityX = 0;
            return;
        }

        rb.linearVelocityX = moveX * moveSpeed;

        if (transform.position.x < -screenBounds.x + shipWidth)
        {
            moveTo(new Vector2(-screenBounds.x + shipWidth, posY));
        }
        else if (transform.position.x > screenBounds.x - shipWidth)
        {
            moveTo(new Vector2(screenBounds.x - shipWidth, posY));
        }
    }

    void moveTo(Vector2 pos)
    {
        transform.position = pos;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (isShipDestroied)
        {
            return;
        }
        moveX = context.ReadValue<Vector2>().x;
        
    }

    public void FireLaser(InputAction.CallbackContext context)
    {
        if (isShipDestroied)
        {
            return;
        }
        if (context.performed)
        {
            if (Time.time > nextLaserTime)
            {
                SoundManager.Play("Laser");
                nextLaserTime = Time.time + laserDelay;
                GameObject laser = LaserPool.LaserGet();
                laser.transform.position = transform.position;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Missile"))
        {
            StartCoroutine(ShipDestoried());
        }
    }

    IEnumerator ShipDestoried()
    {
        SoundManager.Play("ShipDestoried");
        animator.SetBool("isShipDestoried", true);
        isShipDestroied = true;
        yield return new WaitForSeconds(1f);
        animator.SetBool("isShipDestoried", false);
        isShipDestroied = false;
        OnShipDestoried.Invoke();
    }
}
