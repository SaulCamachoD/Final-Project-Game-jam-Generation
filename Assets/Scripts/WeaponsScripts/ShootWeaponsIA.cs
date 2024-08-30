using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWeaponsIA : MonoBehaviour
{
    public string targetLayerName = "Target"; // Nombre de la capa del objetivo
    public float detectionRadius = 50f; // Radio de detección
    public float shootDistance = 20f; // Distancia para disparar
    public float SpeedShoot = 3f;
    public bool isIAWeapons;
    private Transform target;

    // Cambiado a la clase base SpwanBullets para mayor flexibilidad
    public SpwanBullets shootRocket;
    private bool isShooting = false;
    private void Start()
    {
        Transform grandparent = transform.parent?.parent;
        if (grandparent != null && grandparent.CompareTag("Car"))
        {
            isIAWeapons = true;
        }
        else
        {
            isIAWeapons = false;
        }
    }
    void Update()
    {
        DetectAndAim();
    }

    void DetectAndAim()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, LayerMask.GetMask(targetLayerName));

        foreach (Collider hitCollider in hitColliders)
        {
            if (!IsChildOf(hitCollider.transform, transform))
            {
                target = hitCollider.transform;
                Vector3 direction = (target.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(direction);

                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (distanceToTarget <= shootDistance && isIAWeapons && !isShooting)
                {
                    isShooting = true;
                    shootRocket.HandleShooting();
                    StartCoroutine(ResetShootFlag());
                }

                break;
            }
        }

        if (hitColliders.Length == 0 || target == null)
        {
            target = null;
        }
    }

    bool IsChildOf(Transform potentialChild, Transform parent)
    {
        while (potentialChild != null)
        {
            if (potentialChild == parent)
                return true;
            potentialChild = potentialChild.parent;
        }
        return false;
    }

    IEnumerator ResetShootFlag()
    {
        yield return new WaitForSeconds(SpeedShoot);
        isShooting = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
   
}
