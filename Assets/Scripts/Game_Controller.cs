using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add TextMeshPro namespace

using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl; // Import SceneManagement namespace for scene management
public class Game_Controller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //public TextMeshProUGUI highScoreText; // Change to TextMeshProUGUI component
    public TextMeshProUGUI scoreText; // Change to TextMeshProUGUI component

    public int highScore = 0; // Variable to store the high score
    public int score = 0; // Variable to store the high score



    public Score_Manager scoreManager; // Reference to the Score_Manager script


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //highScore = PlayerPrefs.GetInt("HighScore", 0); // Retrieve the high score from PlayerPrefs
        score = scoreManager.score; // Get the current score from the Score_Manager script

        //highScoreText.text = "High Score: " + highScore.ToString(); // Update the high score UI text
        scoreText.text = "BandidoPoints: " + score.ToString(); // Update the current score UI

    }
    
    public void Restart()
    {
        // Reset the score and high score
        score = 0;
        //highScore = 0;
        Debug.Log("Restarting game." );

        // Update the UI text components
        //scoreText.text = "Puntos1: " + score;
        //highScoreText.text = "High Score: " + highScore;

        // Reload the current scene to restart the game
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("MainGameScene");
    }
    public void goToMenu()
    {
        // Reset the score and high score
        score = 0;
        highScore = 0;
        Debug.Log("Restarting game." );

        // Update the UI text components
        //scoreText.text = "Puntos2: " + score;
        //highScoreText.text = "High Score: " + highScore;

        // Reload the current scene to restart the game
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("HomeScreen", LoadSceneMode.Single);
    }
}
