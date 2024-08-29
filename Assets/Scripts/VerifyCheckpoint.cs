using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyCheckpoint : MonoBehaviour
{
    public List<GameObject> carsPassed = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        GameObject car;

        Debug.Log("carro");

        if (other.CompareTag("Car") || other.CompareTag("Player"))
        {
            car = other.gameObject;

        }
        else

        {
            return;

        }



        if (!carsPassed.Contains(car))
        {
            carsPassed.Add(car);
            if (other.CompareTag("Player"))

            {
                car.GetComponent<RaceCarProgress>().UpdatePosition(carsPassed.Count);
            }

        }





    }


}
