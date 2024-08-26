using UnityEngine;

public class CarSelector : MonoBehaviour
{
    public GameObject[] cars;
    public int currentCarIndex = 0;

    void Start()
    {
        ShowCar(currentCarIndex);
    }

    public void NextCar()
    {
        cars[currentCarIndex].SetActive(false);
        currentCarIndex = (currentCarIndex + 1) % cars.Length;
        ShowCar(currentCarIndex);
    }

    public void PreviousCar()
    {
        cars[currentCarIndex].SetActive(false);
        currentCarIndex = (currentCarIndex - 1 + cars.Length) % cars.Length;
        ShowCar(currentCarIndex);
    }

    private void ShowCar(int index)
    {
        cars[index].SetActive(true);
        CarSelectionData.selectedCarIndex = index;
    }
}