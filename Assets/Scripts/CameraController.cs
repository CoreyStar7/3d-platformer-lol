using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Variables
    public Transform target;

    public Vector3 offset;

    public bool useOffsetValues;

    public float rotateSpeed;

    public Transform pivot;

    // Start is called before the first frame update
    void Start()
    {
        if(!useOffsetValues) // if NOT useOffsetValues then set the offset based on how far the Camera and Character is at the start of the game ("!" means not)
        { 
        offset = target.position - transform.position; // Set the camera at the start of the session
        }

        pivot.transform.position = target.transform.position; // Move the pivot to wherever the player is on Game Boot
        pivot.transform.parent = target.transform; // Changing parents from Main Camera to Player on Game Boot

        Cursor.lockState = CursorLockMode.Locked; // Hide the cursor pointer on Game Boot
    }

    // Update is called once per frame
    void LateUpdate()
    {

        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed; // Get the X Axis of the Computer's Attached Physical Mouse and Rotate the Camera Target
        target.Rotate(0, horizontal, 0);

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate(-vertical, 0, 0); // Get the Y position of the mouse and rotate the pivot

        float desiredYAngle = target.eulerAngles.y; // Move the camera based on the current rotation of the targeet and the original offset
        float desiredXAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0); // Tracking the Cursor in Software
        transform.position = target.position - (rotation * offset);

        // transform.position = target.position - offset; // Changing the camera position and offset depending on the Character's position

        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z); 
        }

        transform.LookAt(target); // Focus camera on Character
    }
}
