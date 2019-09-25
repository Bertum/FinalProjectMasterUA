using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneScript : MonoBehaviour {

    private BaseProceduralScene bPS = new BaseProceduralScene();
    float offset;

    // Start is called before the first frame update
    void Start() {
        bPS.start();        
    }

    // Update is called once per frame
    void Update() {
        MoveSkyBox();
    }

    private void MoveSkyBox() {
        GameObject dome = GameObject.FindGameObjectWithTag("Sky");
        Renderer domeRenderer = dome.GetComponent<Renderer>();
        offset += Time.deltaTime / (48);
        domeRenderer.material.mainTextureOffset = new Vector2(offset, 0);
    }

}
