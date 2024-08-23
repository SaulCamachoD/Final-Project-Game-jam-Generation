using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanBullets : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float munition = 4f;
    protected bool _canShoot;
    protected private void Start()
    {
        _canShoot = true;
      
    }

    protected virtual void Update()
    {   
        if (Input.GetButtonDown("Fire1") && _canShoot)
        {
            Shoot();
            _canShoot=false;
            StartCoroutine(WaitShoot());
            munition--;

            if (munition <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    protected virtual void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletSpeed, ForceMode.Impulse);
    }

    protected virtual IEnumerator WaitShoot()
    {
        yield return new WaitForSeconds(3);
        _canShoot = true;
    }
}
