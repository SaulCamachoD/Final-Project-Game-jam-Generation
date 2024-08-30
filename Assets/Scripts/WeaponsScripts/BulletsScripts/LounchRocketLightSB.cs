using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LounchRocketLightSB : SpwanBullets
{
    public Transform firePoint2;
    private bool useFirstFirePoint = true;

    protected override void Shoot()
    {

        Transform selectedFirePoint = useFirstFirePoint ? firePoint : firePoint2;
        useFirstFirePoint = !useFirstFirePoint;


        GameObject bullet = Instantiate(bulletPrefab, selectedFirePoint.position, selectedFirePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(selectedFirePoint.forward * bulletSpeed, ForceMode.Impulse);
    }

    protected override IEnumerator WaitShoot()
    {
        yield return new WaitForSeconds(1.5f);
        _canShoot = true;
    }
}
