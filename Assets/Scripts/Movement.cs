using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Speed of rotation
    float rocketRotate = 230f;
    //main speed of rocket
    float mainThrust = 1000f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainFire;
    [SerializeField] ParticleSystem leftFire;
    [SerializeField] ParticleSystem rightFire;

    Rigidbody rbody;
    AudioSource audioSource;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }
        else
        {
            StopVFX();
        }
    }

    void StartThrusting()
    {
        rbody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainFire.isPlaying)
        {
            mainFire.Play();
        }
    }

    void StopThrusting()
    {
        mainFire.Stop();
        audioSource.Stop();
    }

    void StopVFX()
    {
        rightFire.Stop();
        leftFire.Stop();
    }

    void RotateLeft()
    {
        if (!leftFire.isPlaying)
        {
            leftFire.Play();
        }
        toiletRotate(-rocketRotate);
    }

    void RotateRight()
    {
        if (!rightFire.isPlaying)
        {
            rightFire.Play();
        }
        toiletRotate(rocketRotate);
    }

    void toiletRotate(float rotationDir)
    {
        rbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationDir);
        rbody.freezeRotation = false;
    }
}
