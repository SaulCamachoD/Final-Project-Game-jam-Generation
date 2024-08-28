using UnityEngine;

public class VehicleSoundManager : MonoBehaviour
{
    public AudioSource engineStartSound;
    public AudioSource idleSound;
    public AudioSource accelerationSound;
    public AudioSource brakeSound;


    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Reproducir sonido de encendido
        engineStartSound.Play();

        // Luego de un pequeño delay, activar el sonido de ralentí
        Invoke("PlayIdleSound", engineStartSound.clip.length);
    }

    void PlayIdleSound()
    {
        idleSound.Play();
    }


    void Update()
    {
        float speed = rb.velocity.magnitude;

        // Sonido de ralentí
        if (speed < 0.1f)
        {
            if (!idleSound.isPlaying)
            {
                idleSound.Play();
                accelerationSound.Stop();
                brakeSound.Stop();
            }
        }
        // Sonido de aceleración
        else if (Input.GetAxis("Vertical") > 0)
        {
            if (!accelerationSound.isPlaying)
            {
                accelerationSound.Play();
                idleSound.Stop();
                brakeSound.Stop();
            }
        }
        // Sonido de frenado
        else if (Input.GetAxis("Vertical") < 0)
        {
            if (!brakeSound.isPlaying)
            {
                brakeSound.Play();
                idleSound.Stop();
                accelerationSound.Stop();
            }
        }
    }
}
