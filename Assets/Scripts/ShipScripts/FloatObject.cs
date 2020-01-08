using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatObject : MonoBehaviour {
    public float waterLevel = 3.0f;
    public float floatThreshold = 0.1f;
    public float downForce = 0.125f;

    float forceFactor;
    Vector3 floatForce;

   private void FixedUpdate() {
        //Go Down
        if(transform.position.y > waterLevel + floatThreshold || transform.position.y > waterLevel - floatThreshold) {
            floatForce = new Vector3(0.0f, -downForce, 0.0f);
        }
        //Go Up
        else if (transform.position.y < waterLevel - floatThreshold || transform.position.y < waterLevel + floatThreshold) {
            floatForce = new Vector3(0.0f, downForce, 0.0f);
            
        }
        GetComponent<Rigidbody>().AddForce(floatForce);
    }

}