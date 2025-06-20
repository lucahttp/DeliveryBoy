using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
public class Player_Movement : MonoBehaviour
{
    public Transform transform;
    public float speed = 5f;
    public float rotationSpeed = 5f;

    void Start()
    {
        Debug.Log(Time.timeScale);
    }
    void Update()
    {

        Movement();
        Clamp();

    }



    void Movement()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -47), rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 47), rotationSpeed * Time.deltaTime);

        }
        if (transform.rotation.z != 90)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), 10f * Time.deltaTime);

        }
    }
    void Clamp()
    {
        /*
if (transform.position.x <-1.62f)
{
    transform.position = new Vector3(-1.62f, transform.position.y, transform.position.z);
}


if (transform.position.x > 1.62f)
{
    transform.position = new Vector3(1.62f, transform.position.y, transform.position.z);
}
*/
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -1.62f, 1.62f);
        transform.position = pos;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cars")
            Debug.Log(collision.gameObject.name);
            Time.timeScale = 0;      
    }
}