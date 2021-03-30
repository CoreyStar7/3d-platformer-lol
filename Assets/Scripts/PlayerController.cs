using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    public float moveSpeed;
    public float jumpHeight;
    public CharacterController CC;
    
    private Vector3 moveDirection;
    public float gravityScale;

    // Start is called before the first frame update
    void Start()
    {
        CC = GetComponent<CharacterController>(); // As soon as the Game starts, find Component "CharacterController"
    }

    // Update is called once per frame
    void Update()
    {
        // moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed); // Obtain the CharacterController (CC) Horizontal Axis (X Value) and Vertical Axis (Z Value) multiply it's default move speed (1) to our set 16 in Unity

        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical") * moveSpeed) + (transform.right * Input.GetAxisRaw("Horizontal") * moveSpeed); // Same input as above however it listens to the camera's properties
        // moveDirection = moveDirection.normalized * moveSpeed; // Prevents two inputs from increasing speed accidently
        moveDirection.y = yStore;


        if (CC.isGrounded) // Check if Player is touching a floor surface
        {
            moveDirection.y = 0f; // Allows a more realistic fall rather then a straight dive downward
        if (Input.GetButtonDown("Jump")) // Ask Unity if the Jump Alias is being pressed
            { 
            moveDirection.y = jumpHeight; // Balancing the Y Character Axis to the JumpHeight Value
            }
        }

        moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime); // Equal jumpHeight to the Gravity Value set in Unity, adding in correct gravity and jumping physics (No need for DeltaTime due to the next line using DeltaTime)
        CC.Move(moveDirection * Time.deltaTime); // Times the MoveDirection by Delta Time to stabilize the movement correctly to all clients (ALWAYS USE DELTATIME TO STABILIZE CLIENT MOVEMENT!)
    }
}
