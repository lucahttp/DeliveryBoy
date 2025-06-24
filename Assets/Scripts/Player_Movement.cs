using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
public class Player_Movement : MonoBehaviour
{
    public Transform transform;
    public float speed = 5f;
    public float rotationSpeed = 5f;

    public Score_Manager scoreValue;


    public GameObject gameOverPanel; // Reference to the Game Over panel
    void Start()
    {
        Debug.Log(Time.timeScale);
        gameOverPanel.SetActive(false);
        Time.timeScale = 1; // Ensure the game is running at normal speed
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


        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -90), rotationSpeed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 90), rotationSpeed * Time.deltaTime);

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


        pos.y = Mathf.Clamp(pos.y, -4.3f, 4.3f);

        transform.position = pos;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cars")
        {
            Debug.Log("Collision with car detected!");
            Debug.Log(collision.gameObject.name);
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        else if (collision.gameObject.tag == "Coins")
        {
            Debug.Log("Coin collected!");
            // Handle coin collection, e.g., increase score, play sound, etc.
            // For example, you could destroy the coin:
            scoreValue.score += 10; // Assuming you have a method to increase the score
            Destroy(collision.gameObject);

        }
    }
}