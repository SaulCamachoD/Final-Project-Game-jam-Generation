using System.Collections;
using UnityEngine;

public class TommyGunLightSB : SpwanBullets
{
    public AudioSource shoot;
    public ParticleSystem Fire;
    protected override void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
        shoot.Play();
        Fire.Play();
    }
    protected override IEnumerator WaitShoot()
    {
        yield return new WaitForSeconds(0.8f);
        _canShoot = true;
    }
}
