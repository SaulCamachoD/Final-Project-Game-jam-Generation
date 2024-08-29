using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController1 : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject player;
    public GameObject powerUpsweapon;
    public float distanceToWeapon = 0f;
    public float speedMovement = 2f;
    public float speedRotation = 5f;

    [Header("Patrol and Follow Settings")]
    public Transform[] patrolPoints;
    private int currentPointIndex = 0;

    [Header("Race Settings")]
    public Transform[] racePoints;
    private int currentRacePointIndex = 0;

    [Header("Weapons Control")]
    public GameObject WeaponSpawnPoint;

    [Header("Enemy States")]
    [SerializeField] private EnemyStates currentState;


    private Rigidbody rb;
    private float distanceToPlayer;
    private NavMeshAgent agent;

    private Vector3 nearestAvailablePosition = Vector3.zero;
    public enum EnemyStates
    {
        GetWeapon,
        Attack,
        Patrol,
        MoveToNextPatrolPoint,
        Run
    }

    protected virtual void Awake()
    {
        powerUpsweapon = GameObject.Find("WeaponsSpawnsPoints");
        player = GameObject.FindGameObjectWithTag("Player");
        AssignPatrolAndRacePoints();

    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = EnemyStates.GetWeapon;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        agent = GetComponent<NavMeshAgent>();
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

            case EnemyStates.MoveToNextPatrolPoint:
                MoveToNextPatrolPoint();
                break;

            case EnemyStates.Run:
                GoingTofinish();
                break;


        }
        FindNearestAvailablePosition();
    }

    void FindNearestAvailablePosition()
    {
        float nearestDistance = Mathf.Infinity;

        foreach (var spawnPoint in SpawnPointStatus.spawnPointStatuses)
        {
            Vector3 position = spawnPoint.Key;
            int status = spawnPoint.Value;
            if (status == 1)
            {
                float distance = Vector3.Distance(transform.position, position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestAvailablePosition = position;
                }
            }
        }
    }

    void MoveAWeapons()
    {

        float distance = Vector3.Distance(transform.position, nearestAvailablePosition);
        if (distance > distanceToWeapon)
        {

            agent.SetDestination(nearestAvailablePosition);

        }
        else
        {

            currentState = EnemyStates.Patrol;
        }


    }

    void NormalPatrol()
    {
        GetWeapons();
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= 40f && distanceToPlayer > 30f)
        {
            currentState = EnemyStates.Attack;
        }

        else
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            }
            agent.SetDestination(patrolPoints[currentPointIndex].position);
        }

    }

    void Enemymovement()
    {
        GetWeapons();

        agent.updateRotation = true; // Asegura que el agente maneje la rotaci�n autom�ticamente

        // Moverse hacia el jugador usando NavMeshAgent
        agent.SetDestination(player.transform.position);

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Cambiar de estado si la distancia al jugador es mayor a 20 unidades
        if (distanceToPlayer < 20f)
        {
            currentState = EnemyStates.MoveToNextPatrolPoint;
        }
    }

    void MoveToNextPatrolPoint()
    {
        GetWeapons();

        agent.SetDestination(patrolPoints[currentPointIndex].position);
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentState = EnemyStates.Patrol;
        }

    }
    void GoingTofinish()
    {
        agent.SetDestination(racePoints[currentRacePointIndex].position);

        // Verifica si el agente ha llegado al punto de destino
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            // Avanza al siguiente punto en el arreglo
            currentRacePointIndex = (currentRacePointIndex + 1) % racePoints.Length;


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

    void GetWeapons()
    {
        if (WeaponSpawnPoint.transform.childCount == 0)
        {
            currentState = EnemyStates.GetWeapon;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            StartCoroutine(ResumeMovementAfterDelay());
            currentState = EnemyStates.Patrol;

        }
    }

    private IEnumerator ResumeMovementAfterDelay()
    {
        agent.isStopped = true;  // Detener el agente
        yield return new WaitForSeconds(0.5f); // Esperar medio segundo
        agent.isStopped = false; // Reanudar el agente

        // Establecer el destino al pr�ximo punto de patrullaje
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        agent.SetDestination(patrolPoints[currentPointIndex].position);
    }

    protected virtual void AssignPatrolAndRacePoints()
    {
        // Busca todos los objetos con los tags correspondientes
        GameObject[] racePointsObjects = GameObject.FindGameObjectsWithTag("RacePoint");
        GameObject[] patrolPointsObjects = GameObject.FindGameObjectsWithTag("PatrolPoint");

        // Ordena los objetos por nombre
        System.Array.Sort(racePointsObjects, (a, b) => a.name.CompareTo(b.name));
        System.Array.Sort(patrolPointsObjects, (a, b) => a.name.CompareTo(b.name));

        racePoints = new Transform[racePointsObjects.Length];
        for (int i = 0; i < racePointsObjects.Length; i++)
        {
            racePoints[i] = racePointsObjects[i].transform;
        }

        // Asigna los Transform de esos objetos a los arreglos
        patrolPoints = new Transform[patrolPointsObjects.Length];
        for (int i = 0; i < patrolPointsObjects.Length; i++)
        {
            patrolPoints[i] = patrolPointsObjects[i].transform;
        }

    }
}
