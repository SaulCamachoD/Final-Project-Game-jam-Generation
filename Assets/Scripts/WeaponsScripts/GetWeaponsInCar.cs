using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWeaponsInCar : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject weaponPoint;
    private Dictionary<string, int> weaponTags;

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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (weaponTags.ContainsKey(other.tag) && weaponPoint.transform.childCount == 0)
        {
            int weaponIndex = weaponTags[other.tag];
            GameObject weaponInstance = Instantiate(weapons[weaponIndex], weaponPoint.transform.position, weaponPoint.transform.rotation, weaponPoint.transform);
            weaponInstance.transform.localPosition = Vector3.zero;
        }
    }
    
}
