using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour {

    private List<Animator> animlist;
    public GameObject[] Elements;
    public bool interactive; //if we want door to open on player command
    private bool insideTrigger; //check for input only when inside trigger
    private bool doorOpen; //check if doors are open
    private bool active; //check if doors are opening

    void Start()
    {
        animlist = new List<Animator>();
        for (int i = 0; i < Elements.Length; i++)
        {
            animlist.Add(Elements[i].GetComponent<Animator>());
        }
    }

    void doorConflict()
    {
        active = false;
    }

    void OpenDoors()
    {
        active = true;
        doorOpen = true;
        foreach (Animator anim in animlist)
        {
            anim.SetBool("open", true);
        }
        Invoke("doorConflict", 1);
    }

    void CloseDoors()
    {
        active = true;
        doorOpen = false;
        foreach (Animator anim in animlist)
        {
            anim.SetBool("open", false);
        }
        Invoke("doorConflict", 1);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vehicle2" || other.tag == "Vehicle3" || other.tag == "Player")
        {
            if (!interactive)
            {
                OpenDoors();
            }
            else
            {
                insideTrigger = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Vehicle2" || other.tag == "Vehicle3" || other.tag == "Player")
        {
            if (!interactive)
            {
                CloseDoors();
            }
            else
            {
                insideTrigger = false;
            }
        }
    }

    void Update()
    {
        if (interactive == true)
        {
            if (insideTrigger == true)
            {
                if (Input.GetKeyDown(KeyCode.Return) && doorOpen == false && active == false)
                {
                    OpenDoors();
                }
                if (Input.GetKeyDown(KeyCode.Return) && doorOpen == true && active == false)
                {
                    CloseDoors();
                }
            }
        }
    }
}
