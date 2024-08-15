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

    private float currentSteerAngle;
    private float currentAngleWheels;
    private float currentAcceleration;
    private float currentBrakeForce;
    public bool isBraking;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        currentAcceleration = Input.GetAxis("Vertical") * motorForce;
        currentSteerAngle = Input.GetAxis("Horizontal") * maxSteeringAngle;
        currentAngleWheels = Input.GetAxis("Horizontal") * wheelsAngle;
        isBraking = Input.GetKey(KeyCode.Space);

        frontLeftWheel.localEulerAngles = new Vector3(0, currentAngleWheels, 0);
        frontRightWheel.localEulerAngles = new Vector3(0, currentAngleWheels, 0);
    }

    void FixedUpdate()
    {
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
            Quaternion turnRotation = Quaternion.Euler(0, steerAngle, 0);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}
