using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    // public List<Collider> colliders;
    public float heightThreshold = 500f;

    // Start is called before the first frame update
    void Start()
    {
        // colliders = new List<Collider>();

        if(transform.localScale.z <= heightThreshold) {
            Debug.Log(transform.localScale.z);
            gameObject.tag = "boost";
        }
    }

    // void OnCollisionEnter(Collision other) {
    //     Debug.Log("Collision");
    //     colliders.Add(other.gameObject.GetComponent<Collider>());
    // }

    // public List<Collider> getColliders() {
    //     return colliders;
    // }
}
