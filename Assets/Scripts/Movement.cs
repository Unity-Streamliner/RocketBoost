using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] private float ThrustingSpeed = 1000.0f;
    [SerializeField] private float RotationSpeed = 100f;
    [SerializeField] private AudioClip mainEngineAudio;

    // CACHE - e.g. refereces for readability or speed
    private Rigidbody _rigidbody;
    private AudioSource _audioSource;
    
    // STATE - private instance (member) variables
    bool _isAlive;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProccesThrust();
        ProccesRotation();
    }

    private void ProccesThrust()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            print("Pressed Space - Thrusting");
            Vector3 ThrustingForce = ThrustingSpeed * Time.deltaTime * Vector3.up;
            _rigidbody.AddRelativeForce(ThrustingForce, ForceMode.Acceleration);
            if (!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(mainEngineAudio);
            }
        } else {
            _audioSource.Stop();
        }
    }

    private void ProccesRotation() 
    {
        if (Input.GetKey(KeyCode.A)) 
        {
            ApplyRotation(RotationSpeed);
        } else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-RotationSpeed);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true; // freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        _rigidbody.freezeRotation = false; // unfreeze rotation so the physics system can take over
    }
}
