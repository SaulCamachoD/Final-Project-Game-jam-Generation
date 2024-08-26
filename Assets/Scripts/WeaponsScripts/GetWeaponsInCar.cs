using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWeaponsInCar : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject weaponPoint;
    private Dictionary<string, int> weaponTags;
    private WeaponsSpawnManager weaponsSpawnManager;

    private void Start()
    {
        weaponTags = new Dictionary<string, int>()
        {
            { "MG1", 0 },
            { "MG2", 1 },
            { "AM1", 2 },
            { "AM2", 3 },
            { "AM3", 4 }
        };
        weaponsSpawnManager = FindObjectOfType<WeaponsSpawnManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (weaponTags.ContainsKey(other.tag) && weaponPoint.transform.childCount == 0)
        {
            int weaponIndex = weaponTags[other.tag];
            GameObject weaponInstance = Instantiate(weapons[weaponIndex], weaponPoint.transform.position, weaponPoint.transform.rotation, weaponPoint.transform);
            weaponInstance.transform.localPosition = Vector3.zero;

            // Guardar la posición de spawn para reaparecer el arma
            Vector3 spawnPosition = other.transform.position;

            Destroy(other.gameObject);

            // Iniciar la corrutina para verificar el arma
            StartCoroutine(CheckAndRespawnWeapon(spawnPosition));
        }
    }

    private IEnumerator CheckAndRespawnWeapon(Vector3 spawnPosition)
    {
        // Esperar hasta que no haya más hijos en el weaponPoint (arma utilizada)
        while (weaponPoint.transform.childCount > 0)
        {
            yield return null; // Espera un frame
        }

        // Reaparecer el arma en el punto original
        weaponsSpawnManager.RespawnWeaponAtPoint(spawnPosition);
    }

}
