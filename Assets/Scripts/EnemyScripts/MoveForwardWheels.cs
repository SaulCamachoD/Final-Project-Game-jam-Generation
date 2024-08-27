using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardWheels : MonoBehaviour
{
    public Transform frontLeftWheel;
    public Transform frontRightWheel;

    // �ngulo m�ximo de giro
    public float maxSteerAngle = 30f;

    void Update()
    {
        // Obtener la entrada horizontal del teclado
        float steerInput = Input.GetAxis("Horizontal");

        // Calcular el �ngulo de giro basado en la entrada del usuario
        float steerAngle = steerInput * maxSteerAngle;

        // Aplicar el �ngulo de giro a las ruedas delanteras
        frontLeftWheel.localRotation = Quaternion.Euler(0, steerAngle, 0);
        frontRightWheel.localRotation = Quaternion.Euler(0, steerAngle, 0);
    }
}
