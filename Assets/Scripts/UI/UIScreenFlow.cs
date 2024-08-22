using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenFlow : MonoBehaviour
{
    public GameObject[] screens;

    public void goToMainScreen() 
    {
        InactivateAllScreens();
        screens[0].SetActive(true);
    }

    public void goToCarSelection()
    {
        InactivateAllScreens();
        screens[1].SetActive(true);
    }

    public void InactivateAllScreens()
    {
        foreach (var screen in screens)
        {
            screen.SetActive(false);
        }
    }
}
