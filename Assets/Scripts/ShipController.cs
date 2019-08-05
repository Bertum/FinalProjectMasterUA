using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public Vector3 centerOfMass;
    public float shipSpeed = 15;
    public float movementThreshold = 10.0f;

    Transform m_COM;
    float verticalInput;
    float movementFactor;
    float horizontalInput;
    float steerFactor;

    // Update is called once per frame
    void Update() {
        Balance();
        Movement();
        Steer();
    }

    private void Balance() {
        if (!m_COM) {
            m_COM = new GameObject("COM").transform;
            m_COM.SetParent(transform);
        }
        m_COM.position = centerOfMass;
        GetComponent<Rigidbody>().centerOfMass = m_COM.position;
    }

    private void Movement() {
        verticalInput = Input.GetAxis("Vertical");
        movementFactor = Mathf.Lerp(movementFactor, verticalInput, Time.deltaTime / movementThreshold);
        transform.Translate(0.0f, 0.0f, movementFactor);
    }

    private void Steer() {
        horizontalInput = Input.GetAxis("Horizontal");
        steerFactor = Mathf.Lerp(steerFactor,horizontalInput,Time.deltaTime / movementThreshold);
        transform.Rotate(0.0f, steerFactor * shipSpeed, 0.0f);
    }
}
