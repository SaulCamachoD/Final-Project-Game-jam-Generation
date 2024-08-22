using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectsTheCar : MonoBehaviour
{
    public Transform[] wheels; // Arreglo para las ruedas del carro
    public float rotationSpeed = 500f; // Velocidad de rotación de las ruedas

    void Update()
    {
        // Detectar si se presiona la tecla 'W'
        if (Input.GetKey(KeyCode.W))
        {
            RotateWheels();
        }
    }

    // Función para rotar las ruedas
    void RotateWheels()
    {
        foreach (Transform wheel in wheels)
        {
            wheel.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
    }
}
