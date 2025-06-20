using UnityEngine;

public class Car_Spawner : MonoBehaviour
{
    public GameObject car1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Instantiate(car1, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(car1, transform.position, Quaternion.Euler(0, 0, 90));
        Debug.Log("Car spawned at position: " + car1.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
