using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSpawnManager : MonoBehaviour
{
    public GameObject[] weaponPrefabs;
    public Transform[] spawnPoints;  // Array de posiciones donde las armas deben aparecer
    private Dictionary<Vector3, int> weaponSpawnPoints = new Dictionary<Vector3, int>();
    private Dictionary<int, Transform> spawnPointTransforms = new Dictionary<int, Transform>();

    private void Start()
    {
        // Aparecer las armas en sus puntos de spawn al inicio del juego
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Vector3 spawnPosition = spawnPoints[i].position;
            InstantiateWeaponAt(spawnPoints[i], i);
            weaponSpawnPoints.Add(spawnPosition, i);
            spawnPointTransforms.Add(i, spawnPoints[i]);
        }
    }

    private void Update()
    {
        
        CheckSpawnPoints();
    }

    private void InstantiateWeaponAt(Transform spawnPoint, int index)
    {
        GameObject weaponInstance = Instantiate(weaponPrefabs[index], spawnPoint.position, Quaternion.identity);
        weaponInstance.transform.SetParent(spawnPoint); // Establece el punto de spawn como padre del arma
        weaponInstance.SetActive(true);
    }

    public void RespawnWeaponAtPoint(Vector3 spawnPosition)
    {
        if (weaponSpawnPoints.ContainsKey(spawnPosition))
        {
            int weaponIndex = weaponSpawnPoints[spawnPosition];
            Transform spawnPoint = spawnPointTransforms[weaponIndex];
            InstantiateWeaponAt(spawnPoint, weaponIndex);
        }
    }
    private void CheckSpawnPoints()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            int status = spawnPoint.childCount > 0 ? 1 : 0;
            Vector3 position = spawnPoint.position;

            // Actualiza el diccionario global
            if (SpawnPointStatus.spawnPointStatuses.ContainsKey(position))
            {
                SpawnPointStatus.spawnPointStatuses[position] = status;
            }
            else
            {
                SpawnPointStatus.spawnPointStatuses.Add(position, status);
            }
        }
    }
}
