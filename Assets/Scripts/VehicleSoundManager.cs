using UnityEngine;

public class RealisticEngineSound : MonoBehaviour
{
    public AudioSource idleSound;
    public AudioSource lowRPMSound;
    public AudioSource medRPMSound;
    public AudioSource highRPMSound;
    public AudioSource startupSound;

    private Rigidbody rb;
    private float maxSpeed = 100f;
    private float maxRPM = 7000f; // Simulated maximum RPM.
    private float minRPM = 1000f; // Simulated minimum RPM.
    private bool engineStarted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Start the engine sound when the car spawns.
        StartEngine();
    }

    void Update()
    {
        if (engineStarted)
        {
            float speed = rb.velocity.magnitude;
            float speedPercent = speed / maxSpeed;
            float rpm = Mathf.Lerp(minRPM, maxRPM, speedPercent);

            // Adjust the pitch of the sound based on the RPM.
            AdjustRPMSound(lowRPMSound, speedPercent, 0.0f, 0.33f);
            AdjustRPMSound(medRPMSound, speedPercent, 0.33f, 0.66f);
            AdjustRPMSound(highRPMSound, speedPercent, 0.66f, 1.0f);

            if (Input.GetKey(KeyCode.W))
            {
                if (idleSound.isPlaying)
                {
                    idleSound.Stop();
                }

                if (speedPercent < 0.33f)
                {
                    PlaySound(lowRPMSound);
                    StopSound(medRPMSound);
                    StopSound(highRPMSound);
                }
                else if (speedPercent >= 0.33f && speedPercent < 0.66f)
                {
                    PlaySound(medRPMSound);
                    StopSound(lowRPMSound);
                    StopSound(highRPMSound);
                }
                else
                {
                    PlaySound(highRPMSound);
                    StopSound(lowRPMSound);
                    StopSound(medRPMSound);
                }
            }
            else
            {
                if (!idleSound.isPlaying)
                {
                    idleSound.loop = true;
                    idleSound.Play();
                }
            }
        }
    }

    void StartEngine()
    {
        // Play the startup sound, then play the idle sound.
        startupSound.Play();
        Invoke("PlayIdleSound", startupSound.clip.length);
    }

    void PlayIdleSound()
    {
        idleSound.loop = true;
        idleSound.Play();
        engineStarted = true;
    }

    void AdjustRPMSound(AudioSource sound, float speedPercent, float minSpeed, float maxSpeed)
    {
        if (speedPercent >= minSpeed && speedPercent < maxSpeed)
        {
            sound.pitch = Mathf.Lerp(0.8f, 1.2f, (speedPercent - minSpeed) / (maxSpeed - minSpeed));
        }
    }

    void PlaySound(AudioSource sound)
    {
        if (!sound.isPlaying)
        {
            sound.Play();
        }
    }

    void StopSound(AudioSource sound)
    {
        if (sound.isPlaying)
        {
            sound.Stop();
        }
    }
}
