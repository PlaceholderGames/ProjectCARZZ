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
    private Animator anim;
    private Boolean hitPlayer = false;

    private void Start()
    {
        //anim = GetComponent<Animator>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Player")
            hitPlayer = true;
    }

    void FixedUpdate()

    {
        
        xpos = (target.position.x - transform.position.x);
        zpos = (target.position.z - transform.position.z);
        //if the player is inside the detectable distance
        if ((Math.Abs(xpos) < detectDistance && Math.Abs(zpos) < detectDistance) && !hitPlayer)
        {
            anim.SetBool("isIdle", false);
            Vector3 rot = target.transform.position - transform.position;
            rot.y = 0;
            Quaternion rotation = Quaternion.LookRotation(rot);
            transform.rotation = rotation;
            //The AI gets the position of the player and it goes towards it.
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed);
        }
        else
        {
            anim.SetBool("isIdle", true);
        }
          
    }
}