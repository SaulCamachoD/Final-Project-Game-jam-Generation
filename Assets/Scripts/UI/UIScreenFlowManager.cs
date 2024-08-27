using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScreenFlowManager : MonoBehaviour
{
    public GameObject[] screens;

    public void GoToMainScreen()
    {
        InactivateAllScreens();
        screens[0].SetActive(true);
    }

    public void GoToCarSelection()
    {
        InactivateAllScreens();
        screens[1].SetActive(true);
    }
    public void OnPlayGameButtonPressed()
    {
        SceneManager.LoadScene("GustavoScene");
    }

    public void InactivateAllScreens()
    {
        foreach (var screen in screens)
        {
            screen.SetActive(false);
        }
    }
}

