using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 45f; // Valor más realista
    public float maxSteerAngle = 60f; // Ángulo máximo de giro de las ruedas delanteras
    public float brakeForce = 3000f; // Fuerza de frenado del freno de mano

    public Transform centerOfMass;
    public Transform frontWheels;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
    }

    void FixedUpdate()
    {
        // Obtener el input del jugador
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        print(moveHorizontal);
        print(moveVertical);

        // Mover el carro hacia adelante o atrás usando AddForce
        Vector3 movement = transform.forward * moveVertical * speed;
        rb.AddForce(movement, ForceMode.Acceleration);

        // Rotar las ruedas delanteras y limitar el ángulo de giro
        float steerAngle = moveHorizontal * 350f * Time.deltaTime;
        steerAngle = Mathf.Clamp(steerAngle, -maxSteerAngle, maxSteerAngle);
        frontWheels.localRotation = Quaternion.Euler(0, steerAngle, 0);

        // Aplicar una fuerza lateral de forma más gradual
        float lateralForce = moveVertical * turnSpeed * moveHorizontal * 0.5f; // Ajusta el factor 0.5 según sea necesario
        rb.AddForceAtPosition(transform.right * lateralForce, frontWheels.position);

        // Aplicar freno de mano si se presiona la barra espaciadora
        if (Input.GetKey(KeyCode.Space))
        {
            // Aplicar una fuerza de frenado opuesta a la dirección de movimiento
            Vector3 brakeForceVector = -rb.velocity.normalized * brakeForce;
            rb.AddForce(brakeForceVector);
        }
    }
}

