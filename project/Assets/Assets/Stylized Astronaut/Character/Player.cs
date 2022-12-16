using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private Animator anim;
    private CharacterController controller;

    public float speed = 600.0f;
    public float turnSpeed = 400.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;
		public float jump_force = 20.0f;
    // public TerrainCollider TerrainCollider;
    private Bounds bounds;
    private Vector3 startPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        // bounds = TerrainCollider.bounds;
        startPosition = transform.position;
    }

    // private bool isCharacterTryingToBreakThrough() {

    // }

    void Update()
    {
        // if(!bounds.Contains(transform.position)) {
        //   Debug.Log("PLEASE STOP");
        //   //transform.position = bounds.ClosestPoint(transform.position);
        //   controller.enabled = false;
        //   transform.position = bounds.ClosestPoint(transform.position);
        //   controller.enabled = true;
        // }
        // if(transform.position.y <= 0) {
        //   controller.enabled = false;
        //   transform.position = startPosition;
        //   controller.enabled = true;
        // }
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetInteger("AnimationPar", 1);
        }
				else
        {
            anim.SetInteger("AnimationPar", 0);
        }

        if (controller.isGrounded) {
					moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
					if(Input.GetKeyDown("space")) {
						moveDirection.y += jump_force;
					}
				}

				moveDirection.y -= gravity * Time.deltaTime;		

        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        controller.Move(moveDirection * Time.deltaTime);
    }
}

