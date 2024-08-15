using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesEnemy : MonoBehaviour
{
    public GameObject jugador;
    public float distanciaMaxima = 20f;
    public float velocidadMovimiento = 2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Enemymovement();
    }

    public void Enemymovement()
    {
        float distancia = Vector3.Distance(transform.position, jugador.transform.position);
        if (distancia > distanciaMaxima)
        {
            Vector3 direccion = jugador.transform.position - transform.position;
            direccion.Normalize();
            rb.velocity = direccion * velocidadMovimiento;
        }
    }
}
