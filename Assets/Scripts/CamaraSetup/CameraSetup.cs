using Cinemachine;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    public GameObject carPrefab; // Prefab del carro que se spawnea
    public Transform spawnPoint; // Punto de spawn del carro
    public CinemachineVirtualCamera virtualCamera; // Referencia a la Cinemachine Virtual Camera

    private void Start()
    {
        // Spawnear el carro en el punto de spawn
        GameObject spawnedCar = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);

        // Buscar el objeto hijo llamado "target" en el carro spawneado
        Transform target = spawnedCar.transform.Find("target");

        if (target != null)
        {
            // Asignar el target a las propiedades Follow y Look At de la cámara
            virtualCamera.Follow = target;
            virtualCamera.LookAt = target;
        }
        else
        {
            Debug.LogError("No se encontró un objeto hijo llamado 'target' en el carro spawneado.");
        }
    }
}
