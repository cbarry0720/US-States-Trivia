using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightsRandomizer : MonoBehaviour
{
    public int defaultHeightCoefficient = 1;
    public int heightCap = 10;
    public int heightFloor = 1;

    public float heightThreshold = 500f;

    public List<GameObject> states;

    // public GameObject player;
    // public StarterAssets.ThirdPersonController playerController;

    // private float jumpHeight;

    // Start is called before the first frame update
    void Start()
    {
        // Adding states to a list of states
        foreach(Transform child in transform) {
            // Debug.Log("Child: " + child);
            states.Add(child.gameObject);
        }

        var rand = new System.Random();

        // Randomizing the heights of each state
        foreach(GameObject state in states) {
            float coefficient = rand.Next(heightFloor, heightCap);
            float zScaling = state.transform.localScale.z;

            float xScaling = state.transform.localScale.x;
            float yScaling = state.transform.localScale.y;

            state.transform.localScale = new Vector3(xScaling, yScaling, zScaling * coefficient * defaultHeightCoefficient);

            if(state.transform.localScale.z <= heightThreshold) {
                state.gameObject.tag = "boost";
            }
        }



        // float jumpForce = playerController.JumpHeight;
        // float gravity = playerController.Gravity;
        // // jumpHeight = (Mathf.Sqrt(-2f * jumpForce * gravity) + (0.5f * gravity * Mathf.Pow(Time.deltaTime, 2))) * 1000; 
        // jumpHeight = 1200f;
        // Debug.Log(jumpHeight);

        // // If the state is not adjacent to any states the player
        // // Can jump to, apply tag that allows player to jump higher 
        // // Normal allowing itself to be freed.
        // foreach(Transform child in transform) {
        //     GameObject state = child.gameObject;
        //     if(stateIsATrap(state)) {
        //         // Apply Tag "trap or boost" to state
        //         state.gameObject.tag = "boost";
        //     } else {
        //         state.gameObject.tag = "untagged";
        //     }
        // }
    }

    // bool stateIsATrap(GameObject state) {
    //     List<Collider> colliders = state.gameObject.GetComponent<JumpBoost>().getColliders();

    //     float currentHeight = state.transform.localScale.z;
    //     foreach(Collider c in colliders) {
    //         float adjacentHeight = c.gameObject.transform.localScale.z;

    //         Debug.Log(currentHeight + ", " + adjacentHeight);

    //         if(currentHeight + jumpHeight <= adjacentHeight) {
    //             return false;
    //         }
    //     }

    //     return true;
    // }
}
