using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public float detectionRange = 10f;  // Rango de detecci�n
    public LayerMask targetLayer;       // Capas a detectar (ej. Jugador)

    private Transform closestTarget;    // Referencia al objetivo m�s cercano (jugador)

    void Update()
    {
        FindClosestTarget();

        if (closestTarget != null)
        {
            float distance = Vector3.Distance(transform.position, closestTarget.position);

            if (distance <= detectionRange)
            {
                // Calcular la direcci�n hacia el objetivo m�s cercano
                Vector3 targetDirection = closestTarget.position - transform.position;

                // Ignorar la componente Y para mantener la orientaci�n horizontal
                targetDirection.y = 0;

                if (targetDirection != Vector3.zero)
                {
                    // Calcular la rotaci�n necesaria para apuntar hacia el objetivo en el plano horizontal
                    Quaternion lookRotation = Quaternion.LookRotation(targetDirection);

                    // Aplicar la rotaci�n suavemente
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                }

                // Mostrar mensaje de disparar o cualquier acci�n adicional
                Debug.Log("Disparar");
            }
        }
    }

    void FindClosestTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange, targetLayer);
        float closestDistance = Mathf.Infinity;
        closestTarget = null;

        foreach (Collider collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = collider.transform;
            }
        }
    }
}


