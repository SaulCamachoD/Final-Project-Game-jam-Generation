using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public Transform playerSpawnPoint;
    public Transform[] aiSpawnPoints;
    public GameObject[] carPrefabsPlayer;
    public GameObject[] carPrefabsAI;

    void Start()
    {
        int playerCarIndex = CarSelectionData.selectedCarIndex;

        Instantiate(carPrefabsPlayer[playerCarIndex], playerSpawnPoint.position, playerSpawnPoint.rotation);

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
