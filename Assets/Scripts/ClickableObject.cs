using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableObject : MonoBehaviour {
    // Start is called before the first frame update
    private MouseManager mouseController;
    private EventInformation eventInfo;

    void Start() {
        mouseController = GameObject.FindObjectOfType<MouseManager>();
        eventInfo = GameObject.FindObjectOfType<EventInformation>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnMouseDown() {
        Debug.Log(mouseController.getHoveredObject());
        eventInfo.eventType = 1;
        SceneManager.LoadScene("BaseProceduralScene");
        if (mouseController.getHoveredObject() != null && mouseController.getHoveredObject() == transform.root.gameObject) {
            mouseController.selectedItem = transform.root.gameObject;
        }
    }
}
