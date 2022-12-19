using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightsRandomizer : MonoBehaviour
{
    public int defaultHeightCoefficient = 1;
    public int heightCap = 10;
    public int heightFloor = 1;

    public List<GameObject> states;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform) {
            // Debug.Log("Child: " + child);
            states.Add(child.gameObject);
        }

        var rand = new System.Random();

        foreach(GameObject state in states) {
            float coefficient = rand.Next(heightFloor, heightCap);
            float zScaling = state.transform.localScale.z;

            float xScaling = state.transform.localScale.x;
            float yScaling = state.transform.localScale.y;

            state.transform.localScale = new Vector3(xScaling, yScaling, zScaling * coefficient * defaultHeightCoefficient);
        }
    }
}
