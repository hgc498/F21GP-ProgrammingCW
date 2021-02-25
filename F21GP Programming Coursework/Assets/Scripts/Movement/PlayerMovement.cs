//refrenced from: https://sharpcoderblog.com/blog/third-person-camera-in-unity-3d
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    public float speed = 12f;
    public float jumpSpeed = 2f;
    public float gravity = 9.81f;
    public float jumpHeight= 3f;
    public float sprintSpeed = 3f;



    private Vector3 moveDirection;
    private CharacterController controller;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //check to see if the player is on the ground 
        if (controller.isGrounded)
        {
            //if the player is grounded, set the movement axes to the horizontal and vertical to get the player to move
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            //get the player to jump
            //limiting the heignt and adding a force of gravity 
            //pressing spacebar will get the player to jump
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(jumpSpeed * jumpHeight * gravity);
            }

            //add sprint option
            //set it so that when left shift is held down the player will move fatser 
            if (Input.GetKey(KeyCode.LeftShift) && !(Input.GetButton("Jump")))
            {
                moveDirection *= sprintSpeed;
            }

        }
        //apply gravity 
        moveDirection.y -= gravity * Time.deltaTime;
        //move the player
        controller.Move(moveDirection * Time.deltaTime);

        //rotate player 
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);

    }
}