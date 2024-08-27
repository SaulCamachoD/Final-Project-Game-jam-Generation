using Cinemachine;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public Transform[] aiSpawnPoints;
    public GameObject[] carPrefabsPlayer;
    public GameObject[] carPrefabsAI;
    public CinemachineVirtualCamera cinemachineCamera; // Referencia a la CinemachineVirtualCamera

    void Start()
    {
        int playerCarIndex = CarSelectionData.selectedCarIndex;

        // Instanciar el carro del jugador
        GameObject playerCar = Instantiate(carPrefabsPlayer[playerCarIndex], playerSpawnPoint.position, playerSpawnPoint.rotation);

        // Encontrar el objeto hijo llamado "target" del carro del jugador
        Transform targetTransform = playerCar.transform.Find("Target");

        // Asignar el objeto "target" a los campos Follow y Look At de la cámara
        if (targetTransform != null && cinemachineCamera != null)
        {
            cinemachineCamera.Follow = targetTransform;
            cinemachineCamera.LookAt = targetTransform;
        }
        else
        {
            Debug.LogWarning("Target or CinemachineVirtualCamera not found.");
        }

        // Instanciar los carros de la IA
        int aiSpawnIndex = 0;
        for (int i = 0; i < carPrefabsAI.Length; i++)
        {
            if (i != playerCarIndex)
            {
                Instantiate(carPrefabsAI[i], aiSpawnPoints[aiSpawnIndex].position, aiSpawnPoints[aiSpawnIndex].rotation);
                aiSpawnIndex++;
            }
        }
    }
}
