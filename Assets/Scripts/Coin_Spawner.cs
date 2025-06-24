using System.Collections;
using UnityEngine;

public class Coin_Spawner : MonoBehaviour
{


    public GameObject coinPrefab; // Assign your coin prefab in the inspector
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnCoins());
    }

    // Update is called once per frame
    void Update()
    {

    }


    void CoinSpawn()
    {
        float[] backwardLanes = new float[] { -1.6f, -0.55f };
        float[] forwardLanes = new float[] { 0.55f, 1.6f };

        // Combine all lanes into a single array for simpler random selection
        float[] allLanes = new float[backwardLanes.Length + forwardLanes.Length];
        System.Array.Copy(backwardLanes, 0, allLanes, 0, backwardLanes.Length);
        System.Array.Copy(forwardLanes, 0, allLanes, backwardLanes.Length, forwardLanes.Length);

        // Randomly select a lane from the combined array
        int randomLaneIndex = Random.Range(0, allLanes.Length);
        float selectedLane = allLanes[randomLaneIndex];

        // depending on the lane, set the y position
        float yPosition = 0f; // Default y position
        if (selectedLane < 0)
        {
            yPosition = -1f; // For backward lanes
        }
        else if (selectedLane > 0)
        {
            yPosition = 1f; // For forward lanes
        }



        // Instantiate the coin at a random position within the selected lane
        Instantiate(coinPrefab, new Vector3(selectedLane,yPosition,0), Quaternion.identity);
    }
    

    IEnumerator SpawnCoins()
    {
        while (true)
        {
            CoinSpawn();
            yield return new WaitForSeconds(1f); // Adjust the spawn rate as needed
        }
    }
}
