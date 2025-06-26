using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Credit_Controller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Update is called once per frame
    public void onStartClick()
    {
        Debug.Log("Button clicked!");
        SceneManager.LoadScene("HomeScreen");
    }
}
