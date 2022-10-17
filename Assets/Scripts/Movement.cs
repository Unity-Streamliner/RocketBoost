using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float ThrustingSpeed = 1000.0f;
    [SerializeField] private float RotationSpeed = 100f;
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
    }
}
