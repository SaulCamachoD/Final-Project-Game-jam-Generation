using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelector : MonoBehaviour
{
    public GameObject[] cars;
    private int currentIndex = 0;

    void Start()
    {
        ShowCar(currentIndex);
    }

    public void NextCar()
    {
        cars[currentIndex].SetActive(false);
        currentIndex = (currentIndex + 1) % cars.Length;
        ShowCar(currentIndex);
    }

    public void PreviousCar()
    {
        cars[currentIndex].SetActive(false);
        currentIndex = (currentIndex - 1 + cars.Length) % cars.Length;
        ShowCar(currentIndex);
    }

    private void ShowCar(int index)
    {
        cars[index].SetActive(true);
    }
}