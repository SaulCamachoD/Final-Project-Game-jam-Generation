using TMPro;
using UnityEngine;

public class RaceCarProgress : MonoBehaviour
{
    public TextMeshProUGUI positionText; // TextMesh Pro

    private int currentPosition = 0;

    private void Start()
    {
        GameObject textObject = GameObject.Find("TextPosition");
        positionText = textObject.GetComponent<TextMeshProUGUI>();


    }

    public void SetPositionText(TextMeshProUGUI textMeshProUGUI)
    {
        positionText = textMeshProUGUI;
    }

    public void UpdatePosition(int position)
    {
        currentPosition = position;

        // Actualiza el texto en la UI
        if (positionText != null)
        {
            positionText.text = $"Posición: {currentPosition.ToString()}";


        }
    }

    public void DisableMovement()
    {
        CarControllerStanBy carController = GetComponent<CarControllerStanBy>();
        if (carController != null)
        {
            carController.DisableMovement();
        }
    }
}
