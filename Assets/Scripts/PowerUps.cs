﻿using System.Collections.Generic; using UnityEngine;  public class PowerUps : MonoBehaviour {      public float healthAmount = 1;      private void OnTriggerEnter2D(Collider2D other)     {         Debug.Log("tag = " + other.tag);         if(other.tag == "Player")         {             Player_Controller player = other.gameObject.GetComponent<Player_Controller>();              // Add health to player             if(name.Contains("apple"))             {                 Debug.Log("PINAPPLE!!!");                 player.AddHealth(healthAmount);                 Destroy(gameObject);             }             // Shield player once             if(name.Contains("Broosh"))             {                 Debug.Log("BROOOOOOOSH!!!");                 Destroy(gameObject);             }             // Faster, stronger             if(name.Contains("Cubes"))             {                 Debug.Log("CUUUUUBE!!!");                 player.Shielded();                 Destroy(gameObject);             }         }     } }