using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TommyGunHeavySB : TommyGunMidleSB
{
    // Array para múltiples puntos de spawneo
    public Transform[] firePoints;
    private int currentFirePointIndex = 0;

    protected override void Shoot()
    {
        // Obtenemos el firePoint actual y avanzamos al siguiente
        Transform firePoint = firePoints[currentFirePointIndex];
        currentFirePointIndex = (currentFirePointIndex + 1) % firePoints.Length;

        // Instanciamos la bala en el punto de spawneo actual
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);

        // Activamos las partículas en el punto de spawneo actual
        Fire.transform.position = firePoint.position; // Mover las partículas al punto de spawneo actual
        Fire.Play();
    }
}
