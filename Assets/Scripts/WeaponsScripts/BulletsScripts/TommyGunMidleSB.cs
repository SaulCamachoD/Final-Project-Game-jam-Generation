using System.Collections;
using UnityEngine;

public class TommyGunMidleSB : TommyGunLightSB
{
    private bool _isFiringBurst = false;
    public float burstTime = 4f;
    protected override void Update()
    {
        if (Input.GetButton("Fire1") && !_isFiringBurst)
        {
            StartCoroutine(FireBurst());
        }

        if (!Input.GetButton("Fire1") && shoot.isPlaying)
        {
            shoot.Stop();
        }
    }

    private IEnumerator FireBurst()
    {
        _isFiringBurst = true;

        if (!shoot.isPlaying)
        {
            shoot.Play();
        }
        float elapsedTime = 0f;

        while (elapsedTime < burstTime && Input.GetButton("Fire1"))
        {
            if (_canShoot)
            {
                Shoot();
                _canShoot = false;
                StartCoroutine(WaitShoot());
                munition--;

                if (munition <= 0)
                {
                    shoot.Stop();
                    Destroy(gameObject);
                    yield break;
                }
            }

            elapsedTime += Time.deltaTime;
            yield return null; // Esperar hasta el siguiente frame
        }

        shoot.Stop();
        yield return new WaitForSeconds(0.5f); // Pausa de medio segundo
        _isFiringBurst = false;
    }

    protected override void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
        Fire.Play();
    }

    protected override IEnumerator WaitShoot()
    {
        yield return new WaitForSeconds(0.1f);
        _canShoot = true;
    }
}
