using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    float moveX;
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float shipWidth;
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        shipWidth = GetComponent<Collider2D>().bounds.extents.x;

        rb.linearVelocityX = moveX * moveSpeed;
        float posY = transform.position.y;
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
        moveX = context.ReadValue<Vector2>().x;
        
    }
}
