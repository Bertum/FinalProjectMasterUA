using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesScript : StateMachineBehaviour {
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public int xSize = 40;
    public int zSize = 40;

    private float offSetX = 100f;
    private float offSetZ = 100f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {        
        mesh = new Mesh();
        animator.GetComponent<MeshFilter>().mesh = mesh;
        //GetComponent<MeshFilter>().mesh = mesh;
        offSetX = Random.Range(0, 100f);
        offSetZ = Random.Range(0, 100f);
        createShape();
        updateMesh();
    }

    private void updateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
        
    private void createShape() {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        //Create Vertices
        for (int i = 0, z = 0; z <= zSize; z++) {
            for (int x = 0; x <= xSize; x++) {

                float y = Mathf.PerlinNoise(x * .2f + offSetX, z * .2f + offSetZ) * 2f;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];
        //vertices of a square
        int vert = 0;
        //triangles in a square
        int tris = 0;
        //Build a square
        for (int z = 0; z < zSize; z++) {
            for (int x = 0; x < xSize; x++) {
                //low triangle |_
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                //top triangle -|
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                //6 because 3vertices+3vertices
                tris += 6;
            }
            vert++;
        }

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        offSetX += Time.deltaTime * 2f;
        createShape();
        updateMesh();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
