using UnityEngine;
using UnityEngine.Events;

public class Laser : MonoBehaviour
{
    [SerializeField] float speed = 3.0f;
    float screenHeight;
    public static UnityAction<GameObject> OnRecycleLaser;

    void Start()
    {
        screenHeight = Camera.main.orthographicSize;
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (transform.position.y > screenHeight)
        {
            OnRecycleLaser!.Invoke(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bunker"))
        {
            OnRecycleLaser!.Invoke(gameObject);
            Destroy(other.gameObject);
        }
    }
}
