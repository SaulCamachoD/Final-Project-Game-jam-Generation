using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Weapons Control")]
    public GameObject WeaponSpawnPoint;

    [Header("Enemy States")]
    [SerializeField] private EnemyStates currentState;


    private Rigidbody rb;
    private float  distanceToPlayer;
    public enum EnemyStates
    {
        GetWeapon,
        Attack,
        Patrol,
        MoveToNextPatrolPoint,
        Run
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = EnemyStates.GetWeapon;
        distanceToPlayer = Vector3.Distance (transform.position,player.transform.position);
    }
    void Update()
    {
        print(distanceToPlayer);
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
            currentState = EnemyStates.Patrol;
        }
    }

    void NormalPatrol()
    {
        GetWeapons();
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= 30f && distanceToPlayer > 20f)
        {
            currentState = EnemyStates.Attack;
        }

        Transform targetPoint = patrolPoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speedMovement * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
        Vector3 direction = (targetPoint.position - transform.position).normalized;

        // Comprobar si la dirección no es cero antes de aplicar la rotación
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speedMovement);
        }

    }

    void Enemymovement()
    {
        GetWeapons();
        /// Calcular la dirección hacia el jugador
        Vector3 direction = (player.transform.position - transform.position).normalized;
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        // Comprobar si la dirección no es cero antes de aplicar la rotación
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speedMovement);
        }

        // Moverse hacia el jugador
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speedMovement * Time.deltaTime);

        // Cambiar de estado si la distancia al jugador es mayor a 20 unidades
        if (distanceToPlayer < 10f)
        {
            currentState = EnemyStates.MoveToNextPatrolPoint;
        }
    }

    void MoveToNextPatrolPoint()
    {
        GetWeapons();

        Transform targetPoint = patrolPoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speedMovement * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            currentState = EnemyStates.Patrol;
        }

        Vector3 direction = (targetPoint.position - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speedMovement);
        }
    }
        void GoingTofinish()
    {

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
        if(WeaponSpawnPoint.transform.childCount == 0)
        {
            currentState = EnemyStates.GetWeapon;
        }
    }
}
