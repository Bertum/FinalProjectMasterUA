using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatObject : MonoBehaviour {
    public float waterLevel = 0.0f;
    public float floatThreshold = 2.0f;
    public float waterDensity = 0.125f;
    public float downForce = 0.0f;

    float forceFactor;
    Vector3 floatForce;

    Rigidbody rigidBody;

    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        forceFactor = 1.0f - ((transform.position.y - waterLevel) / floatThreshold);

        if (forceFactor > 0.0f) {
            floatForce = -Physics.gravity * rigidBody.mass * (forceFactor - rigidBody.velocity.y * waterDensity);
            floatForce += new Vector3(0.0f, -downForce * rigidBody.mass, 0.0f);
            rigidBody.AddForceAtPosition(floatForce, transform.position);
        }

    }
}
