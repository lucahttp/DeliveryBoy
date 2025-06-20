using UnityEngine;

public class Car_Movement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform transform;
    public float speed = 5f;

    void Start()
    {
        transform = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0,speed * Time.deltaTime, 0);

    }
}
