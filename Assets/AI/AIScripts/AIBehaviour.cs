using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{

    public Rigidbody rb;
    public Transform target;
    public float moveSpeed = 5f;
    public float detectDistance= 200f;  //the distance in which the AI can follow the player
    float xpos, zpos;
    

    void FixedUpdate()

    {
        xpos = (target.position.x - transform.position.x);
        zpos = (target.position.z - transform.position.z);
        //if the player is inside the detectable distance
        if (Math.Abs(xpos) < detectDistance && Math.Abs(zpos) < detectDistance)
        {
            Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = rotation;
            //The AI gets the position of the player and it goes towards it.
            rb.AddForce(Math.Max(xpos * moveSpeed, moveSpeed) * Time.deltaTime, 0, Math.Max(zpos * moveSpeed, moveSpeed) * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
}