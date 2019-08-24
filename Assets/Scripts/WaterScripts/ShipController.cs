using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public Vector3 centerOfMass;
    public float shipSpeed = 5;
    public float movementThreshold = 10.0f;
    float offset;

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
        DayTime();
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
        transform.Translate(0.0f, 0.0f, movementFactor * shipSpeed);
    }

    private void Steer() {
        horizontalInput = Input.GetAxis("Horizontal");
        steerFactor = Mathf.Lerp(steerFactor, horizontalInput , Time.deltaTime / movementThreshold);
        transform.Rotate(0.0f, steerFactor * shipSpeed, 0.0f);
    }

    private void DayTime() {
        GameObject dome = GameObject.FindGameObjectWithTag("Sky");
        Renderer domeRenderer = dome.GetComponent<Renderer>();
        offset += Time.deltaTime / (24);
        domeRenderer.material.mainTextureOffset = new Vector2(offset,0);
    }

}
