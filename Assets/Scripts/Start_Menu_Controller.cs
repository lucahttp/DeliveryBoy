using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menu_Controller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void onStartClick()
    {
        Debug.Log("Start button clicked, loading SampleScene.");
        //SceneManager.LoadScene("MainGameScene");
        //SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    // Update is called once per frame
    public void onExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = true; // Ensure the editor is in play mode
#endif  
            Debug.Log("Escape key pressed, quitting application.");
            Application.Quit(); // Quit the application
        
    }
}
