using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform lookAt;
    public Transform camTransform;
    public float distance = 2.5f;


    // For zooming in/out with scrollwheel
    public Transform parentObject;
    public float zoomLevel;
    public float maxZoom = 30.0f;
    public float speed = 30.0f;
    public float sensitivity = 1.0f;
    private float zoomPosition;

    private float currentX = 0.0f;
    private float currentY = 45.0f;
    private float sensitivityX = 20.0f;
    private float sensitivityY = 20.0f;

    private void Start()
    {
        camTransform = transform;
    }

    private void Update()
    {
        if(Input.GetMouseButton(1)) {
            currentX += Input.GetAxis("Mouse X")*2;
            currentY -= Input.GetAxis("Mouse Y")*2;
        }

        // distance += Input.mouseScrollDelta.y * sensitivity;
        // distance = Mathf.Clamp(distance, 0, maxZoom);
        // zoomPosition = Mathf.MoveTowards(zoomPosition, distance, speed * Time.deltaTime);
        // transform.position = parentObject.position + (transform.forward * zoomPosition);

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }
}
