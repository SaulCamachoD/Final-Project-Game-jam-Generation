using TMPro;
using UnityEngine;

public class RaceCarProgress : MonoBehaviour
{
    public TextMeshProUGUI positionText; // TextMesh Pro

    private int currentPosition = 0;

    public void SetPositionText(TextMeshProUGUI textMeshProUGUI)
    {
        positionText = textMeshProUGUI;
    }

    public void UpdatePosition(int position)
    {
        currentPosition = position;
        Debug.Log($"{gameObject.name} está en la posición {currentPosition}");

        // Actualiza el texto en la UI
        Debug.Log(positionText);
        if (positionText != null)
        {
            Debug.Log("chamba");
            positionText.text = currentPosition.ToString();
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
