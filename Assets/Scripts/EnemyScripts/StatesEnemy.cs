using Unity.VisualScripting;
using UnityEngine;

public class StatesEnemy : MonoBehaviour
{
    public GameObject player;
    public GameObject[] wayPoints;
    public GameObject powerUpsweapon;
    public float distanceToWeapon = 0f;
    public float maxDistance = 20f;
    public float speedMovement = 2f;
    public float speedRotation = 2f;
    private Rigidbody rb;
    [SerializeField] private EnemyStates currentState;

    public float radioZonaAtaque = 5f; // Distancia para iniciar el ataque
    public float distanciaAlcanzarWaypoint = 0.5f; // Distancia para considerar que se alcanzó el waypoint
    public float bufferZoneRadius = 10f; // Radio de la zona de transición
    private int indiceWaypointActual = 0; // Índice del waypoint actual

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

            case EnemyStates.Attack:
                Enemymovement();
                break;

            case EnemyStates.Patrol:
                ArroundToPatrol();
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
            RotateTowardsMovementDirection(direction);
        }
        else
        {
            currentState = EnemyStates.Attack;
        }
    }

    void Enemymovement()
    {
        float distancia = Vector3.Distance(transform.position, player.transform.position);
        if (distancia > maxDistance)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();
            rb.velocity = direction * speedMovement;
            RotateTowardsMovementDirection(direction);
        }
        else
        {
            currentState = EnemyStates.Patrol;
        }
    }

    void GoingTofinish()
    {
        print("Corriendo a la meta");
    }

    void ArroundToPatrol()
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

    void RotateTowardsMovementDirection(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speedRotation);
        }
    }
}