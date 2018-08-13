﻿using System.Collections; using System.Collections.Generic; using UnityEngine;  public class Enemy_Controller : MonoBehaviour {     // Public variables     public float speed = 1.0f;     public LayerMask enemyMask;     public Transform sightStart, sightEnd;     public bool spotted = false;      // Private variables     Rigidbody2D myBody;     Transform myTrans;     float myWidth, myHeight;      //Player_Controller player;     Rigidbody2D current;      // Use this for initialization     void Start()     {         myTrans = this.transform;         myBody = this.GetComponent<Rigidbody2D>();          // Get width and height of sprite         SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();         myWidth = mySprite.bounds.extents.x;         myHeight = mySprite.bounds.extents.y;          //player = FindObjectOfType<Player_Controller>();         current = GetComponent<Rigidbody2D>();     }      // Update is called once per frame     void FixedUpdate()     {         //NOTE: This script makes use of the .toVector2() extension method.         //Be sure you have the following script in your project to avoid errors         //http://www.devination.com/2015/07/unity-extension-method-tutorial.html          //Use this position to cast the isGrounded/isBlocked lines from         Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;          //Check to see if there's ground in front of us before moving forward         //NOTE: Unity 4.6 and below use "- Vector2.up" instead of "+ Vector2.down"         //Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);         bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);          //Check to see if there's a wall in front of us before moving forward         //Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * .02f);         bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * .02f, enemyMask);          // Falling enemy         if(name.Equals("blob_falling"))         {             Debug.Log("THIS IS " + name);             // Turn off gravity             current.gravityScale = 0f;             Raycasting();         }          // Walking enemy         if(name.Equals("blob_walking"))         {             //If theres no ground, turn around. Or if I hit a wall, turn around             if (!isGrounded || isBlocked)             {                 Vector3 currRot = myTrans.eulerAngles;                 currRot.y += 180;                 myTrans.eulerAngles = currRot;             }              //Always move forward             Vector2 myVel = myBody.velocity;             myVel.x = -myTrans.right.x * speed;             myBody.velocity = myVel;         }     }      public void Hurt()     {         Destroy(this.gameObject);     }      void Raycasting()     {         Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);         spotted = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));         if(spotted)         {             current.gravityScale = 1f;         }     } }