using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICarSelectionScreen : MonoBehaviour
{
    public GameObject selectedButton;
    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(selectedButton);
    }
}
