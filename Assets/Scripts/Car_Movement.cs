using UnityEngine;

public class Car_Movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform transform;
    public float speed = 4f;

    public Vector2 direction = Vector2.down; // Default: top to bottom

    void Start()
    {
        transform = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // Destroy if off-screen (adjust this for your game)
        if (transform.position.y < -8f || transform.position.y > 8f)
        {
            Destroy(gameObject);
        }
    }
}