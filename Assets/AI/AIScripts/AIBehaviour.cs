﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    
    public Rigidbody rb;
    public Transform target;
    public float moveSpeed = 0.011f;
    public float runMoveSpeed = 0.1f;
    public float detectDistance= 200f;  //the distance in which the AI can follow the player
    private float speed;
    float xpos, zpos;
    private MSVehicleControllerFree vehicle;
    private Animator anim;
    private bool inCar = false;
    
    public AICollision aiCollision;

    private void Start()
    {
        
        anim = GetComponentInChildren<Animator>();
        vehicle = FindObjectOfType<MSVehicleControllerFree>();
    }

   

    void FixedUpdate()
    {
            /// Check if player is inside vehicle
        if (vehicle.isInsideTheCar)
            inCar = true;
        else
            inCar = false;

        xpos = (target.position.x - transform.position.x);
        zpos = (target.position.z - transform.position.z);
        /// Checks if the hit animation is playing
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Reaction Hit"))
        { ///Hit animation is not playing
            //if the player is inside the detectable distance
            if ((Math.Abs(xpos) < detectDistance && Math.Abs(zpos) < detectDistance) && !(aiCollision.hitPlayer))
            {
                anim.SetBool("isIdle", false); ///Player in car
                if (inCar)
                {
                    anim.SetBool("isRunning", true); //Zombie runs
                    speed = runMoveSpeed;
                }
                else
                {
                    anim.SetBool("isWalking", true); //Zombie walks
                    speed = moveSpeed;
                }
                Vector3 rot = target.transform.position - transform.position;
                rot.y = 0;
                Quaternion rotation = Quaternion.LookRotation(rot);
                transform.rotation = rotation;
                //The AI gets the position of the player and it goes towards it.
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
            }
            else //Resets to idle animation if they're out of the range
            {
                anim.SetBool("isIdle", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
            }
        }
        else
        {
            anim.SetBool("isHit", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }

    }
}