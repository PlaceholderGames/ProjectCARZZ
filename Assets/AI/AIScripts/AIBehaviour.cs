using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBehaviour : MonoBehaviour
{
    public float killDistance = 100.0f;
    public Rigidbody rb;
    public Transform target;
    public float moveSpeed = 0.011f;
    public float runMoveSpeed = 0.05f;
    public float detectDistance= 200f;  //the distance in which the AI can follow the player
    private float speed;
    float xpos, zpos;
    private MSVehicleControllerFree vehicle;
    private MSFPSControllerFree player;
    private MSSceneControllerFree sceneController;

    private Animator anim;
    public NavMeshAgent nma;

    bool playerOnce = true;
    bool vehicleOnce = true;
    float timer = 5.0f;


    private void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        vehicle = FindObjectOfType<MSVehicleControllerFree>();
        sceneController = FindObjectOfType<MSSceneControllerFree>();
        player = FindObjectOfType<MSFPSControllerFree>();
        nma.SetDestination(Vector3.zero);
        target = player.transform;
    }

   

    void Update()
    {
        
        timer -= Time.deltaTime;
        Debug.Log(timer);
        if ((!vehicle.isInsideTheCar) && timer < 0.0f)
        //if(player.isActiveAndEnabled)
        {
            if(playerOnce)
            {
                player = FindObjectOfType<MSFPSControllerFree>();
                target = player.transform;
                nma.speed = 3.5f;
                playerOnce = false;
            }
            timer = 5.0f;
        }
        else if (vehicle.isInsideTheCar && timer < 0.0f)
        {
            if(vehicleOnce)
            {
                vehicle = FindObjectOfType<MSVehicleControllerFree>();
                target = vehicle.transform;
                nma.speed = 0.8f;
                vehicleOnce = false;
            }
            timer = 5.0f;
        }
        xpos = (target.position.x - transform.position.x);
        zpos = (target.position.z - transform.position.z);
        float distance = Vector3.Distance(target.position, transform.position);
            //if the player is inside the detectable distance
            //if ((Math.Abs(xpos) < detectDistance && Math.Abs(zpos) < detectDistance))//&& !(aiCollision.hitPlayer)
            if(distance < detectDistance)
            {
                anim.SetBool("isIdle", false); ///Player in car
                if (!vehicle.isInsideTheCar)
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
            //Vector3 rot = target.transform.position - transform.position;
            //rot.y = 0;
            //Quaternion rotation = Quaternion.LookRotation(rot);
            //transform.rotation = rotation;
            ////The AI gets the position of the player and it goes towards it.
            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed);
                nma.SetDestination(target.position);
            }
            else //Resets to idle animation if they're out of the range
            {
                anim.SetBool("isIdle", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                nma.ResetPath();
            }
    }
}