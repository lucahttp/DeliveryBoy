using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Road_Movement : MonoBehaviour
{
    public Renderer meshRenderer;
    public float speed = 1f;

    public float totalDistance = 0f; // Total distance the road has moved

    public delegate void MileageChanged(float mileage);
    public event MileageChanged OnMileageChanged;

    void Update()
    {
        float distanceThisFrame = speed * Time.deltaTime;
        meshRenderer.material.mainTextureOffset += new Vector2(0, distanceThisFrame);
        totalDistance += distanceThisFrame;

        // Notify listeners (e.g., Score_Manager) of mileage update
        OnMileageChanged?.Invoke(totalDistance);
    }
}