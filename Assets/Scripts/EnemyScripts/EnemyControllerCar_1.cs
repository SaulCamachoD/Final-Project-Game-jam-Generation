using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerCar_1 : EnemyController1
{
    protected override void Awake() // override para sobrescribir el método
    {
        // Llama a la versión base de Awake para inicializar todo lo demás
        base.Awake();

        // Asigna los nuevos puntos de patrullaje y carrera usando las nuevas tags
        AssignPatrolAndRacePoints();
    }

    protected override void AssignPatrolAndRacePoints() // override para sobrescribir el método
    {
        // Implementación modificada
        GameObject[] patrolPointsObjects = GameObject.FindGameObjectsWithTag("PatrolPoint_1");
        GameObject[] racePointsObjects = GameObject.FindGameObjectsWithTag("RacePoint");

        patrolPoints = new Transform[patrolPointsObjects.Length];
        for (int i = 0; i < patrolPointsObjects.Length; i++)
        {
            patrolPoints[i] = patrolPointsObjects[i].transform;
        }

        racePoints = new Transform[racePointsObjects.Length];
        for (int i = 0; i < racePointsObjects.Length; i++)
        {
            racePoints[i] = racePointsObjects[i].transform;
        }
    }
}
