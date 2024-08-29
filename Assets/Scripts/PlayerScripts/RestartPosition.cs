using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    public float idleTimeLimit = 3f;  // Tiempo máximo que puede estar quieto antes de ser teletransportado

    public float flipThreshold = 45f;  // Ángulo en grados para considerar que el coche está volcado


    private Vector3 lastPosition;
    private float idleTime;
    private Rigidbody rb;
    private Transform lastCheckpoint;  // Referencia al último checkpoint tocado


    // Start is called before the first frame update
    void Start()
    {

        lastPosition = transform.position;
        idleTime = 0f;
        rb = GetComponent<Rigidbody>();  // Obtiene la referencia al Rigidbody del coche


    }

    // Update is called once per frame
    void Update()
    {
        // Calcula la distancia entre la posición actual y la última posición
        if (Vector3.Distance(transform.position, lastPosition) < 0.01f)
        {
            // Si la distancia es pequeña, incrementa el tiempo de inactividad
            idleTime += Time.deltaTime;
        }
        else
        {
            // Si se mueve, reinicia el tiempo de inactividad
            idleTime = 0f;
        }


        // Verifica si el coche se ha volcado 
        // Si el tiempo de inactividad excede el límite, teletransporta el objeto al punto de control
        if (IsCarFlipped() || idleTime > idleTimeLimit)
        {
            Respawn();
        }


        // Actualiza la última posición
        lastPosition = transform.position;

    }

    bool IsCarFlipped()
    {
        float rotationX = Mathf.Abs(transform.eulerAngles.x);
        float rotationZ = Mathf.Abs(transform.eulerAngles.z);

        // Ajuste para ángulos mayores de 180 grados
        if (rotationX > 180) rotationX = 360 - rotationX;
        if (rotationZ > 180) rotationZ = 360 - rotationZ;

        return rotationX > flipThreshold || rotationZ > flipThreshold;
    }

    void Respawn()
    {

        if (lastCheckpoint != null)
        {
            // Reinicia la velocidad y la rotación del Rigidbody
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Teletransporta al coche al punto de control y restablece la rotación
            transform.position = checkpoint.position;
            transform.rotation = checkpoint.rotation;
            idleTime = 0f;  // Reinicia el tiempo de inactividad tras la teletransportación
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el carro ha tocado un checkpoint
        if (other.CompareTag("Checkpoint"))
        {
            lastCheckpoint = other.transform;  // Actualiza el último checkpoint tocado
        }
    }


}