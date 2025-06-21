using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add TextMeshPro namespace

public class Score_Manager : MonoBehaviour
{



    public int score = 0;
    //public Text scoreText; // UI Text component to display the score
    public TextMeshProUGUI scoreText; // Change to TextMeshProUGUI component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start the Score coroutine to increment the score
        StartCoroutine(Score());
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score; // Update the UI Text with the current score
    }


    IEnumerator Score()
    {
        while (true) // Infinite loop to keep incrementing the score
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second before incrementing the score
            score += 1; // Increment score every frame
            Debug.Log("Current Score: " + score); // Log the current score to the console  
        }
    }
}
