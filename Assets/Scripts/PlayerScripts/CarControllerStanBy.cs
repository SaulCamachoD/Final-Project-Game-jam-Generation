using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerStanBy : MonoBehaviour
{
    public Rigidbody rb;

    public float motorForce = 1500f;
    public float maxSteeringAngle = 30f;
    public float maxSpeed = 50f;
    public float wheelsAngle = 45f;
    public float drag = 1f;
    public float brakeForce = 10000f;

    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLeftWheel;  // Referencia a la rueda trasera izquierda
    public Transform rearRightWheel; // Referencia a la rueda trasera derecha

    private float currentSteerAngle;
    private float currentAngleWheels;
    private float currentAcceleration;
    private float currentBrakeForce;
    public bool isBraking;
    private bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Ajustar el centro de masa para mayor estabilidad
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void Update()
    {
        if (!canMove) return;


        currentAcceleration = Input.GetAxis("Vertical") * motorForce;
        currentSteerAngle = Input.GetAxis("Horizontal") * maxSteeringAngle;
        currentAngleWheels = Input.GetAxis("Horizontal") * wheelsAngle;
        isBraking = Input.GetKey(KeyCode.Space);

        float rotationSpeed = rb.velocity.magnitude * (Input.GetAxis("Vertical") >= 0 ? 1 : -1);
        RotateWheels(rotationSpeed);

        // Limitar la velocidad de giro basada en la velocidad del vehículo
        float speedFactor = Mathf.Clamp01(rb.velocity.magnitude / maxSpeed);
        currentSteerAngle *= (1f - speedFactor * 0.5f);  // Reduce el ángulo de giro a altas velocidades
    }

    void FixedUpdate()
    {
        if (!canMove) return;

        HandleMotor();
        HandleSteering();
    }

    private void HandleMotor()
    {
        Vector3 moveDirection = transform.forward;
        if (Input.GetAxis("Vertical") < 0)
        {
            moveDirection = -transform.forward;
        }

        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(transform.forward * currentAcceleration, ForceMode.Force);
        }

        if (isBraking)
        {
            rb.AddForce(-transform.forward * brakeForce, ForceMode.Force);
        }
    }

    private void HandleSteering()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            float steerAngle = currentSteerAngle;

            // Ajustar el �ngulo de giro en reversa
            if (Input.GetAxis("Vertical") < 0)
            {
                steerAngle = -currentSteerAngle;
            }

            Quaternion turnRotation = Quaternion.Euler(0, steerAngle, 0);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }

    private void RotateWheels(float rotationSpeed)
    {
        // Ajusta la rotación en el eje X para simular el movimiento
        frontLeftWheel.Rotate(Vector3.right, rotationSpeed);
        frontRightWheel.Rotate(Vector3.right, rotationSpeed);

        // Ajusta la rotación en el eje X para las ruedas traseras
        rearLeftWheel.Rotate(Vector3.right, rotationSpeed);
        rearRightWheel.Rotate(Vector3.right, rotationSpeed);
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    public void EnableMovement()
    {
        canMove = true;
    }



}
