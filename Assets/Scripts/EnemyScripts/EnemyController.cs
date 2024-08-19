using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject player;
    public GameObject powerUpsweapon;
    public float distanceToWeapon = 0f;
    public float speedMovement = 2f;

    [Header("Patrol and Follow Settings")]
    public float bufferZoneRadius = 10f;
    private int indiceWaypointActual = 0;
    public float distanciaAlcanzarWaypoint = 0.5f;
    public GameObject[] wayPoints;

    [Header("Enemy States")]
    [SerializeField] private EnemyStates currentState;


    private Rigidbody rb;
    public enum EnemyStates
    {
        GetWeapon,
        Attack,
        Patrol,
        Run
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = EnemyStates.GetWeapon;
    }
    void Update()
    {
        switch (currentState)
        {
            case EnemyStates.GetWeapon:
                MoveAWeapons();
                break;

            case EnemyStates.Patrol:
                NormalPatrol();
                break;

            case EnemyStates.Attack:
                Enemymovement();
                break;


            case EnemyStates.Run:
                GoingTofinish();
                break;
        }
    }

    void MoveAWeapons()
    {
        float distance = Vector3.Distance(transform.position, powerUpsweapon.transform.position);
        if (distance > distanceToWeapon)
        {
            Vector3 direction = powerUpsweapon.transform.position - transform.position;
            direction.Normalize();
            rb.velocity = direction * speedMovement;
        }
        else
        {
            currentState = EnemyStates.Attack;
        }
    }

    void NormalPatrol()
    {
        // Verificar si el jugador está dentro de la zona de ataque
        float distanciaAlJugador = Vector3.Distance(transform.position, player.transform.position);
        if (distanciaAlJugador < bufferZoneRadius)
        {
            currentState = EnemyStates.Attack;
            return;
        }

        // Lógica de patrulla usando waypoints
        Vector3 objetivoActual = wayPoints[indiceWaypointActual].transform.position;
        Vector3 direccion = objetivoActual - transform.position;
        direccion.Normalize();

        // Actualizar movimiento y rotación
        rb.velocity = direccion * speedMovement;
        //RotateTowardsMovementDirection(direccion);

        // Verificar si se alcanzó el waypoint y actualizar el índice
        if (Vector3.Distance(transform.position, objetivoActual) < distanciaAlcanzarWaypoint)
        {
            indiceWaypointActual = (indiceWaypointActual + 1) % wayPoints.Length;
        }
    }

    void Enemymovement()
    {

    }

    void GoingTofinish()
    {

    }
}
