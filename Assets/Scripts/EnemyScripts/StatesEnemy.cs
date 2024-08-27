using UnityEngine;
using UnityEngine.AI;

public class StatesEnemy : MonoBehaviour
{
    public GameObject player;
    public GameObject[] wayPoints;
    public GameObject powerUpsweapon;
    public float distanceToWeapon = 0f;
    public float maxDistance = 20f;
    private NavMeshAgent agent;
    private Rigidbody rb;
    [SerializeField] private EnemyStates currentState;

    public float distanciaAlcanzarWaypoint = 0.5f;
    public float bufferZoneRadius = 10f;
    private int indiceWaypointActual = 0;

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
        rb.isKinematic = true; // Hacer el Rigidbody kinematic para evitar conflictos
        agent = GetComponent<NavMeshAgent>();
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
            agent.SetDestination(powerUpsweapon.transform.position);
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
            agent.SetDestination(player.transform.position);
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
        float distanciaAlJugador = Vector3.Distance(transform.position, player.transform.position);
        if (distanciaAlJugador < bufferZoneRadius)
        {
            currentState = EnemyStates.Attack;
            return;
        }

        if (!agent.pathPending && agent.remainingDistance < distanciaAlcanzarWaypoint)
        {
            indiceWaypointActual = (indiceWaypointActual + 1) % wayPoints.Length;
            agent.SetDestination(wayPoints[indiceWaypointActual].transform.position);
            currentState = EnemyStates.Patrol;
        }
    }

    
}