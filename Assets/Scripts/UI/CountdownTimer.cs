using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText; // Asigna aqu� el TextMesh Pro para la cuenta regresiva
    public RaceTimer raceTimer; // Asigna aqu� el script del cron�metro

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        int countdown = 3;

        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false); // Oculta el texto "GO!"
        raceTimer.StartTimer(); // Inicia el cron�metro
    }
}