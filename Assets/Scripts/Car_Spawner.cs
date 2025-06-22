using System.Collections;
using System.Collections.Generic;
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

    // NUEVO: Lista ponderada de lanes
    private List<float> _weightedLanes;

    void Start()
    {
        CalculateCarProbabilities();

        // Combine all lanes into a single array for simpler random selection
        _allLanes = new float[backwardLanes.Length + forwardLanes.Length];
        System.Array.Copy(backwardLanes, 0, _allLanes, 0, backwardLanes.Length);
        System.Array.Copy(forwardLanes, 0, _allLanes, backwardLanes.Length, forwardLanes.Length);

        // NUEVO: Crear la lista ponderada
        _weightedLanes = new List<float>();
        // Backward lanes 3 veces cada uno
        foreach (float lane in backwardLanes)
        {
            _weightedLanes.Add(lane);
            _weightedLanes.Add(lane);
            _weightedLanes.Add(lane);
            _weightedLanes.Add(lane);
            //_weightedLanes.Add(lane);
        }
        // Forward lanes 1 vez cada uno
        foreach (float lane in forwardLanes)
        {
            _weightedLanes.Add(lane);
            _weightedLanes.Add(lane);
        }

        if (_weightedLanes == null || _weightedLanes.Count == 0)
        {
            Debug.LogError("No lanes available to spawn cars. Check lane arrays.");
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
        if (_weightedLanes == null || _weightedLanes.Count == 0)
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
        int laneIndex = Random.Range(0, _weightedLanes.Count);
        float selectedLaneXPosition = _weightedLanes[laneIndex];
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
        Vector2 carSpawnPosition;
        if (!isForwardLane)
        {
            carRotation = Quaternion.Euler(0, 0, 180); // Backward direction
            carSpawnPosition = new Vector2(selectedLaneXPosition, transform.position.y); // Y actual del spawner
        }
        else
        {
            carRotation = Quaternion.Euler(0, 0, 0); // Forward direction
            carSpawnPosition = new Vector2(selectedLaneXPosition, -6f); // Y fijo para forward
        }

        // Instanciar el auto
        GameObject spawnedCar = Instantiate(cars[carIndexToSpawn], carSpawnPosition, carRotation);

        // Obtener el script Car_Movement del auto instanciado
        Car_Movement carMovement = spawnedCar.GetComponent<Car_Movement>();

        if (!isForwardLane)
        {
            carMovement.speed = 7f;
            carMovement.direction = Vector2.down;
        }
        else
        {
            carMovement.speed = 2.5f;
            carMovement.direction = Vector2.up;
        }
    }

    IEnumerator SpawnCars()
    {
        while (true)
        {
            SpawnSpecificCar();
            yield return new WaitForSeconds(1f);
        }
    }
}