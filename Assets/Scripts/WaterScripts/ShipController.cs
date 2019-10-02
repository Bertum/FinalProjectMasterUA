using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    public float shipSpeed = 20;
    float offset;

    float verticalInput;
    float movementFactor;
    float horizontalInput;
    float steerFactor;
    Rigidbody rigidBody;

    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        Movement();
        DayTime();
    }

    private void Movement() {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        if (verticalInput > 0) {
            rigidBody.velocity = transform.forward * verticalInput * shipSpeed;
        }
        if (horizontalInput > 0) {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * shipSpeed, Space.World);
        } else if (horizontalInput < 0) {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * shipSpeed, Space.World);
        }
    }

    private void DayTime() {
        GameObject dome = GameObject.FindGameObjectWithTag("Sky");
        Renderer domeRenderer = dome.GetComponent<Renderer>();
        offset += Time.deltaTime / (24);
        domeRenderer.material.mainTextureOffset = new Vector2(offset, 0);
    }

}
