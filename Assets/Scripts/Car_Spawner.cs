using System.Collections;
using UnityEngine;

public class Car_Spawner : MonoBehaviour
{
    public GameObject[] cars;

    [Header("Car Spawn Probabilities")]
    [Range(0.01f, 1f)]
    public float probabilityDecayFactor = 0.8f;

    [Header("Lane Configurations")]
    public float[] backwardLanes = new float[] { -1.6f, -0.55f };
    public float[] forwardLanes = new float[] { 0.55f, 1.6f };

    private float[] _cumulativeCarProbabilities;
    private float[] _allLanes; // Combined array of all lane positions

    void Start()
    {
        CalculateCarProbabilities();

        // Combine all lanes into a single array for simpler random selection
        // Create a new array that can hold all backward and forward lanes
        _allLanes = new float[backwardLanes.Length + forwardLanes.Length];

        // Copy backward lanes
        System.Array.Copy(backwardLanes, 0, _allLanes, 0, backwardLanes.Length);
        // Copy forward lanes after backward lanes
        System.Array.Copy(forwardLanes, 0, _allLanes, backwardLanes.Length, forwardLanes.Length);

        if (_allLanes == null || _allLanes.Length == 0)
        {
            Debug.LogError("Both forward and backward lane arrays are empty! Cannot spawn car.");
            // Consider stopping the coroutine if no lanes are available
            StopAllCoroutines();
            return;
        }

        StartCoroutine(SpawnCars());
    }

    void CalculateCarProbabilities()
    {
        if (cars == null || cars.Length == 0)
        {
            _cumulativeCarProbabilities = new float[0];
            Debug.LogWarning("No car prefabs assigned. Cannot calculate probabilities. Please assign car prefabs.");
            return;
        }

        float[] rawWeights = new float[cars.Length];
        float totalWeight = 0f;

        for (int i = 0; i < cars.Length; i++)
        {
            rawWeights[i] = Mathf.Pow(probabilityDecayFactor, i);
            totalWeight += rawWeights[i];
        }

        _cumulativeCarProbabilities = new float[cars.Length];
        float currentCumulative = 0f;

        for (int i = 0; i < cars.Length; i++)
        {
            currentCumulative += rawWeights[i] / totalWeight;
            _cumulativeCarProbabilities[i] = currentCumulative;
        }
    }

    void SpawnSpecificCar()
    {
        if (_cumulativeCarProbabilities == null || _cumulativeCarProbabilities.Length == 0)
        {
            Debug.LogError("Car probabilities not calculated or no cars assigned. Cannot spawn car.");
            return;
        }
        if (_allLanes == null || _allLanes.Length == 0)
        {
            Debug.LogError("No lanes available to spawn cars. Check lane arrays.");
            return;
        }

        float randomCarTypeSelector = Random.Range(0f, 1f);
        int carIndexToSpawn = -1;

        for (int i = 0; i < _cumulativeCarProbabilities.Length; i++)
        {
            if (randomCarTypeSelector < _cumulativeCarProbabilities[i])
            {
                carIndexToSpawn = i;
                break;
            }
        }

        if (carIndexToSpawn < 0 || carIndexToSpawn >= cars.Length || cars[carIndexToSpawn] == null)
        {
            Debug.LogError($"Invalid car index ({carIndexToSpawn}) or null prefab. Ensure car prefabs are assigned.");
            return;
        }

        // --- Lane Selection and Rotation ---
        int laneIndex = Random.Range(0, _allLanes.Length);
        float selectedLaneXPosition = _allLanes[laneIndex];
        Quaternion carRotation;

        // Determine rotation based on which lane array the selected position belongs to
        bool isForwardLane = false;
        foreach (float fLane in forwardLanes)
        {
            if (Mathf.Approximately(selectedLaneXPosition, fLane))
            {
                isForwardLane = true;
                break;
            }
        }

        if (isForwardLane)
        {
            carRotation = Quaternion.Euler(0, 0, 0); // Forward direction
        }
        else
        {
            carRotation = Quaternion.Euler(0, 0, 180); // Backward direction
        }

        GameObject spawnedCar = Instantiate(cars[carIndexToSpawn], new Vector2(selectedLaneXPosition, transform.position.y), carRotation);


    }

    IEnumerator SpawnCars()
    {
        while (true)
        {
            SpawnSpecificCar();
            yield return new WaitForSeconds(2f);
        }
    }
}
