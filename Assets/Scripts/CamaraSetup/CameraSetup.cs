using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    public GameObject[] carPrefabs; // Asigna aquí los prefabs de los carros.
    private CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        if (carPrefabs.Length == 0)
        {
            Debug.LogError("No car prefabs assigned.");
            return;
        }

        // Encuentra el carro spawneado en la escena.
        GameObject car = FindActiveCar();

        if (car != null)
        {
            // Busca el hijo "Target" dentro del GameObject del carro.
            Transform targetTransform = car.transform.Find("Target");

            if (targetTransform != null)
            {
                // Asigna el hijo "Target" como objetivo de la cámara.
                virtualCamera.Follow = targetTransform;
                virtualCamera.LookAt = targetTransform;
            }
            else
            {
                Debug.LogError("Target not found within the car.");
            }
        }
        else
        {
            Debug.LogError("Car not found in the scene.");
        }
    }

    // Método para encontrar el carro activo en la escena.
    private GameObject FindActiveCar()
    {
        foreach (var prefab in carPrefabs)
        {
            if (GameObject.Find(prefab.name) != null)
            {
                return GameObject.Find(prefab.name);
            }
        }
        return null;
    }
}
