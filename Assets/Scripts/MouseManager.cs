using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {
    // Start is called before the first frame update
    private GameObject hoveredItem;
    public GameObject selectedItem;

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo)) {

            GameObject hitObject = hitInfo.transform.root.gameObject;

            SelectObject(hitObject);

        }
        else {
            ClearHovered();
        }
        
    }

    private void SelectObject(GameObject obj) {
        if(hoveredItem != null) {
            if(obj == hoveredItem) {
                return;
            }
            ClearHovered();
        }
        hoveredItem = obj;
    }    

    public void ClearHovered() {
        hoveredItem = null;
    }

    public GameObject getHoveredObject() {
        return hoveredItem;
    }
}
