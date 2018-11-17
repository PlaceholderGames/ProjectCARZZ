using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class crawlerBehaviour : MonoBehaviour
{

    
    public Transform ctarget;
    private Transform tempTarget;
    public float cmoveSpeed = 0.011f;
    private MSVehicleControllerFree cvehicle;
    private Animator canim;
    private GameObject activeVehicle;


    public AICollision caiCollision;

    private void Start()
    {
        tempTarget = ctarget;
        canim = GetComponentInChildren<Animator>();
        cvehicle = FindObjectOfType<MSVehicleControllerFree>();
    }



    void FixedUpdate()
    {
        activeVehicle = GameObject.FindGameObjectWithTag("Vehicle");
        if (cvehicle.isInsideTheCar)
        {
            tempTarget = activeVehicle.transform;
        }
        else
        {
            tempTarget = ctarget;
        }
        //if the player is inside the detectable distance
        if (!(caiCollision.hitPlayer))
        {
            Vector3 rot = tempTarget.transform.position - transform.position;
            rot.y = 0;
            Quaternion rotation = Quaternion.LookRotation(rot);
            transform.rotation = rotation;
            //The AI gets the position of the player and it goes towards it.
            transform.position = Vector3.MoveTowards(transform.position, tempTarget.transform.position, cmoveSpeed);
        }
        else //Resets to idle animation if they're out of the range or the player is hit
        {
        }
    }
}