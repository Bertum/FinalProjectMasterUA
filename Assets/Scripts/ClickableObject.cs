using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour {
    // Start is called before the first frame update
    private MouseManager mouseController;

    void Start() {
        mouseController = GameObject.FindObjectOfType<MouseManager>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnMouseDown() {
        Debug.Log(mouseController.getHoveredObject());
        if (mouseController.getHoveredObject() != null && mouseController.getHoveredObject() == transform.root.gameObject) {
            mouseController.selectedItem = transform.root.gameObject;
        }
    }
}
