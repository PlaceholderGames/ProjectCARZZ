using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class AIBehaviour : MonoBehaviour
{
    
    public Rigidbody rb;
    public Transform target;
    public float moveSpeed = 0.02f;
    public float detectDistance= 200f;  //the distance in which the AI can follow the player
    float xpos, zpos;
    //public bool isMoving = false;
    

    void FixedUpdate()
    {
        xpos = (target.position.x - transform.position.x);
        zpos = (target.position.z - transform.position.z);
        //if the player is inside the detectable distance
        if (Math.Abs(xpos) < detectDistance && Math.Abs(zpos) < detectDistance)
        {
            Vector3 rot = target.transform.position - transform.position;
            rot.y = 0;
            Quaternion rotation = Quaternion.LookRotation(rot);
            transform.rotation = rotation;
            //The AI gets the position of the player and it goes towards it.
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed);
            //isMoving = true;
        }
    }
}