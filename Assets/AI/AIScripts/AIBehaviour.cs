using System.Collections;
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
    private MSSceneControllerFree sceneController;

    private Animator anim;
    public AICollision aiCollision;

    private void Start()
    {
        
        anim = GetComponentInChildren<Animator>();
        vehicle = FindObjectOfType<MSVehicleControllerFree>();
        sceneController = FindObjectOfType<MSSceneControllerFree>();
    }

   

    void FixedUpdate()
    {
        xpos = (target.position.x - transform.position.x);
        zpos = (target.position.z - transform.position.z);
            //if the player is inside the detectable distance
            if ((Math.Abs(xpos) < detectDistance && Math.Abs(zpos) < detectDistance))//&& !(aiCollision.hitPlayer)
            {
                anim.SetBool("isIdle", false); ///Player in car
                if (sceneController.vehicleCode.isInsideTheCar)
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isRunning", true); //Zombie runs
                    speed = runMoveSpeed;
                }
                else
                {
                    anim.SetBool("isWalking", true); //Zombie walks
                    anim.SetBool("isRunning", false);
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
}