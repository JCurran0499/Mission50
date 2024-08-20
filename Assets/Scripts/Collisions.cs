using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip landing;
    [SerializeField] ParticleSystem crashParticles;

    Quaternion originalRot;
    Rigidbody rb;

    void Start()
    {
        originalRot = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (
            (collision.gameObject.tag.Equals("Edge") || collision.gameObject.tag.Equals("Floor")) && 
            manager.GameOngoing() && manager.CrashesEnabled()
        )
        {
            Debug.Log("You crashed...");
            StartCrashSequence();
        }


        else if (collision.gameObject.tag.Equals("Finish") && 
            manager.AllFuelRetrieved() && manager.GameOngoing()
        )
        {
            Debug.Log("You landed with the fuel!");
            StartLandingSequence();   
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Fuel"))
        {
            manager.CollectFuel(other.gameObject);
        }
    }


    // Helper Methods \\
    private void StartLandingSequence()
    {
        transform.rotation = originalRot;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        audioSource.PlayOneShot(landing);
        manager.Win();
    }

    private void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        crashParticles.Play();
        audioSource.PlayOneShot(crash, 0.1f);
        manager.Lose();
    }
}
