using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustRate = 1500;
    [SerializeField] float rotateRate = 50;
    [SerializeField] ParticleSystem thrusterParticles;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void OnDisable()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        if (thrusterParticles.isPlaying)
        {
            thrusterParticles.Stop();
        }
    }


    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustRate * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            if (!thrusterParticles.isPlaying)
            {
                thrusterParticles.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            if (thrusterParticles.isPlaying)
            {
                thrusterParticles.Stop();
            }
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            ApplyRotation(rotateRate);
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            ApplyRotation(-rotateRate);
        }
    }

    private void ApplyRotation(float rotationFrameRate)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward, rotationFrameRate * Time.deltaTime);
        rb.constraints = (
            RigidbodyConstraints.FreezeRotationX | 
            RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezePositionZ
        );
    }
}
