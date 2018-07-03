﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

    // Public variables
    public float topSpeed = 2.0f;       // How fast the player can move
    public float jumpForce = 3.0f;      // Force applied to player when jumping
    public Transform groundCheck;       // Transform at player's feet to check if player is grounded
    public LayerMask whatIsGround;      // What layer is considered the ground
    public float groundRadius = 0.2f;   // The radius of the circle used for checking the distance to the ground
    public LayerMask playerMask;
    public bool canMoveInAir = true;

    // Private variables
    private float move;                 // Float holding move direction
    private bool facingRight = true;    // Is the player facing right
    private Animator animator;          // Reference to animator
    private bool isGrounded = false;    // Not grounded by default

    Rigidbody2D myBody;
    Transform myTrans, tagGround;

    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        myTrans = this.transform;
        tagGround = GameObject.Find(this.name + "/tag_ground").transform;

        animator = GetComponent<Animator>();
        groundCheck = GetComponent<Transform>();
        whatIsGround = 8;
        Debug.Log(whatIsGround.value);
    }

    // Physics will be manipulated at the end of each fram in fixed update
    void FixedUpdate()
    {
        //Debug.Log(groundCheck.position);
        // Will return true or false depending on whether groundCheck hit whatIsGround with the groundRadius
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        // Will return true or false depending on wether the player collids with something or not
        isGrounded = Physics2D.Linecast(myTrans.position, tagGround.position, playerMask);

        //animator.SetBool("Ground", isGrounded);
        //animator.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y); 

        Move(Input.GetAxis("Horizontal"));
        // Get move directions
        //move = Input.GetAxis("Horizontal");

        // Check if player is jumping
        if (Input.GetButtonDown("Jump"))
            Jump();

        /*
        // Adds velocity to the Rigidbody in the move direction multiplied with speed.
        myBody.velocity = new Vector2(move * topSpeed, myBody.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(move));

        // Check if the sprite needs to be flipped
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
        */

        /*
        // Can the player jump, if yes: add jump force
        if (isGrounded && Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetBool("Grounded", false);
            myBody.AddForce(new Vector2(0, jumpForce));
        }
        */
    }

    void Update()
    {

    }

    public void Move(float horizontalInput)
    {
        // Check if the player is in the air
        if (!canMoveInAir && !isGrounded)
            return;

        // Adds velocity to the Rigidbody in the move direction multiplied with speed.
        myBody.velocity = new Vector2(horizontalInput * topSpeed, myBody.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Check if the sprite needs to be flipped
        if (horizontalInput > 0 && !facingRight)
            Flip();
        else if (horizontalInput < 0 && facingRight)
            Flip();
    }

    public void Jump()
    {
        if(isGrounded)
            myBody.velocity += jumpForce * Vector2.up;
    }


    /// <summary>
    /// Flips the sprite over the x axis
    /// </summary>
    void Flip()
    {
        facingRight = !facingRight;

        // Get local scale and flip over the x axis
        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        // Update local scale
        transform.localScale = theScale;

    }
}