using System.Collections;
using TMPro;
using UnityEngine;

public class ChronometerStartRun : MonoBehaviour
{
    public float timeInSeconds = 60f; // Tiempo total en segundos
    public TextMeshProUGUI timerText; // Referencia al TextMeshPro para mostrar el tiempo
    private bool isTimeOver = false;

    // Evento para notificar cuando el tiempo se haya acabado
    public delegate void TimeOverHandler();
    public event TimeOverHandler OnTimeOver;

    void Start()
    {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        while (timeInSeconds > 0)
        {
            yield return new WaitForSeconds(1f);
            timeInSeconds--;
            UpdateTimerDisplay();
        }

        isTimeOver = true;
        UpdateTimerDisplay();

        // Llama al evento cuando el tiempo se haya acabado
        if (OnTimeOver != null)
        {
            OnTimeOver();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public bool IsTimeOver()
    {
        return isTimeOver;
    }
}
