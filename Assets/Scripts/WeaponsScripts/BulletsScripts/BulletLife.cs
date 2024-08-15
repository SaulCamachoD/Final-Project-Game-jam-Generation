using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLife : MonoBehaviour
{
    public float speed = 100f;
    public GameObject impact;
    public AudioSource impactSound;
    private bool hasCollided = false;
    private Rigidbody rb;

     void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

    }
    private void Update()
    {
        Destroy(gameObject, 4f); 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided)
        {
            hasCollided = true;
            impactSound.Play();
            Explode();
            Destroy(gameObject, impactSound.clip.length); // Destruir después de que el sonido termine
        }
    }

    private void Explode()
    {
        if (impact != null)
        {
            GameObject newExplosion = Instantiate(impact, transform.position, impact.transform.rotation);
            Destroy(newExplosion, 3f); 
        }
    }
}
