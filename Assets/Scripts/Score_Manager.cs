using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add TextMeshPro namespace

public class Score_Manager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI mileageText; // Add this in the inspector
    public TextMeshProUGUI gameOverMileageLabel; // Add this in the inspector
    public Road_Movement roadMovement; // Assign this in the inspector

    private float mileage = 0f;
    private bool thankYouShown = false; // Add this variable
    public GameObject thankYouPanelPrefab; // Assign the prefab in the inspector
    private GameObject thankYouPanelInstance; // Store the instance

    private bool thankYouMoving = false;
    public float thankYouSpeed = 2f; // Adjust as needed

    public Transform playerTransform; // Assign this in the inspector

    public GameObject congratulationsPanel; // Reference to the Game Over panel
    public TextMeshProUGUI gameOverLabel; // Reference to the Game Over panel text
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start the Score coroutine to increment the score
        StartCoroutine(Score());

        // Subscribe to the mileage event
        if (roadMovement != null)
        {
            roadMovement.OnMileageChanged += UpdateMileage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "BandidoPoints: " + score;
        if (mileageText != null)
            mileageText.text = "Distancia: " + mileage.ToString("F1") +" km"; // Update mileage text;
            gameOverMileageLabel.text = "Distancia: " + mileage.ToString("F1") +" km"; // Update the high score UI text


        // Move thankYouPanelInstance if needed
        if (thankYouMoving)
        {
            Vector3 pos = thankYouPanelInstance.transform.position;
            pos.y -= thankYouSpeed * Time.unscaledDeltaTime; // Move down (opposite to street)
            if (playerTransform != null && pos.y <= playerTransform.position.y)
            {
                pos.y = playerTransform.position.y;
                thankYouMoving = false;
                gameOverMileageLabel.text = "Distancia: " + mileage.ToString("F1") +" km"; // Update the high score UI text
                Debug.Log("Order Delivered detected!");
                gameOverLabel.text = "¡Pedido Entregado!"; // Update the high score UI text
                Time.timeScale = 0; // Stop the game

                congratulationsPanel.SetActive(true);
            }
            thankYouPanelInstance.transform.position = pos;
        }
    }

    void UpdateMileage(float newMileage)
    {
        mileage = newMileage;
        if (!thankYouShown && mileage >= 3f)
        {
            Debug.Log("Mileage reached 3 miles, showing thank you panel.");
            thankYouShown = true;
            if (thankYouPanelPrefab != null)
            {
                thankYouPanelInstance = Instantiate(
                    thankYouPanelPrefab,
                    new Vector2(2.713f, 5.71f),
                    Quaternion.identity
                );
                thankYouMoving = true; // Start moving
            }
            else
            {
                Debug.LogError("Thank You Panel Prefab is not assigned in the inspector.");
            }
        }
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
