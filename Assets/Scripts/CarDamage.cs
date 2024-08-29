using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDamage : MonoBehaviour
{
    public float knockbackForce = 100f; // Fuerza del retroceso

    // Método cuando el carro recibe un balazo
    public void ReceiveShot(Vector3 shotDirection)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Aplica fuerza de retroceso en la dirección opuesta al disparo
            Vector3 knockbackDirection = -shotDirection.normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
    }

    //  Detecta la colisión con una bala
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Obtiene la dirección del disparo
            Vector3 shotDirection = collision.relativeVelocity;

            // Llama al método de retroceso
            ReceiveShot(shotDirection);
        }
    }

}
