using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VerifyLastCheckpoint : MonoBehaviour
{
    public List<RaceCarProgress> carsPassed = new List<RaceCarProgress>();
    public bool isFinalCheckpoint = false; // Marca si este es el último checkpoint
    public TextMeshProUGUI finalPositionsText; // Referencia al TextMeshPro para mostrar posiciones finales
    private static List<string> finalResults = new List<string>(); // Lista global para almacenar posiciones finales

    private void OnTriggerEnter(Collider other)
    {
        RaceCarProgress car = other.GetComponent<RaceCarProgress>();
        if (car != null && !carsPassed.Contains(car))
        {
            carsPassed.Add(car);
            car.UpdatePosition(carsPassed.Count);

            if (isFinalCheckpoint)
            {
                HandleFinalCheckpoint(car);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RaceCarProgress car = other.GetComponent<RaceCarProgress>();
        if (car != null && carsPassed.Contains(car))
        {
            carsPassed.Remove(car);
            car.DisableMovement(); // Desactiva el movimiento al salir del checkpoint
        }
    }

    private void HandleFinalCheckpoint(RaceCarProgress car)
    {
        // Añadir el coche a los resultados finales
        finalResults.Add($"{car.name}: Posición {carsPassed.Count}");

        // Detener el coche
        car.DisableMovement();

        // Si todos los coches han pasado, mostrar la lista final
        if (finalResults.Count == carsPassed.Count)
        {
            ShowFinalPositions();
        }
    }

    private void ShowFinalPositions()
    {
        string finalText = "Posiciones Finales:\n";
        foreach (string result in finalResults)
        {
            finalText += result + "\n";
        }

        if (finalPositionsText != null)
        {
            finalPositionsText.text = finalText;
        }
    }
}
