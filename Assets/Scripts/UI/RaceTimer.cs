using UnityEngine;
using TMPro;

public class RaceTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Asigna aquí el TextMesh Pro para el cronómetro
    public int startMinutes = 2; // Minutos iniciales configurables desde el Inspector
    public int startSeconds = 0; // Segundos iniciales configurables desde el Inspector

    private float remainingTime;
    private bool isRunning = false;

    private void Start()
    {
        // Convierte los minutos y segundos iniciales en segundos totales
        remainingTime = startMinutes * 60 + startSeconds;
        UpdateTimerDisplay(); // Muestra el tiempo inicial desde el principio
    }

    private void Update()
    {
        if (isRunning && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerDisplay();

            if (remainingTime <= 0)
            {
                // El cronómetro ha llegado a 0
                isRunning = false;
                timerText.text = "00:00:000";
                // Aquí puedes agregar lógica para cuando el tiempo se acabe
            }
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);
        int milliseconds = Mathf.FloorToInt((remainingTime * 100f) % 100f);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}
