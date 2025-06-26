using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Controller1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHomeClick()
    {
        Debug.Log("Home Button clicked!");
        SceneManager.LoadScene("HomeScreen");
    }
}
