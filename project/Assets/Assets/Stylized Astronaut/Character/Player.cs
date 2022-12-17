using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    private Animator anim;
    private CharacterController controller;

    public float speed = 50.0f;
    public float turnSpeed = 400.0f;
    private Vector3 moveDirection = Vector3.zero;
    public float gravity = 20.0f;
		public float jump_force = 3.0f;
    // public TerrainCollider TerrainCollider;
    private Bounds bounds;
    private Vector3 startPosition;

    public Camera mainCamera;
    private Vector3 camForward;
    private bool canJump;
    public float distToGround = 1f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        // bounds = TerrainCollider.bounds;
        startPosition = transform.position;
        camForward = mainCamera.GetComponent<Transform>().forward;
        canJump = true;
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
        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)
          || Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow)
          || Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)
          || Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow)
        )
        {
            anim.SetInteger("AnimationPar", 1);
        }
				else
        {
            anim.SetInteger("AnimationPar", 0);
        }

        moveDirection = ( Input.GetAxis("Vertical") + Input.GetAxis("Horizontal") ) * camForward * speed;
        // moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * camForward * speed;

        canJump = Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);

        if(canJump) {
          if(Input.GetKeyDown("space")) {
            canJump = false;
            moveDirection.y += jump_force;
          }
        }



        // if (controller.isGrounded) {
				// 	moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
				// 	if(Input.GetKeyDown("space")) {
				// 		moveDirection.y += jump_force;
				// 	}
				// }

				moveDirection.y -= gravity * Time.deltaTime;		

        // float turn = Input.GetAxis("Horizontal");
        // transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
        // transform.rotation = Quaternion.LookRotation(moveDirection.normalized);
        controller.Move(moveDirection * Time.deltaTime);
    }
}

