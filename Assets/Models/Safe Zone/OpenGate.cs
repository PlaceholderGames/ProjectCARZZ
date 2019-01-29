using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour {


    private List<Animator> animlist;
    public GameObject[] Elements;

    void Start()
    {

        animlist = new List<Animator>();
        for (int i = 0; i < Elements.Length; i++)
        {
            animlist.Add(Elements[i].GetComponent<Animator>());
        }
    }

    void OpenDoors()
    {
        foreach (Animator anim in animlist)
        {
            anim.SetBool("open", true);
        }
    }

    void CloseDoors()
    {
        foreach (Animator anim in animlist)
        {
            anim.SetBool("open", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vehicle2" || other.tag == "Vehicle3")
        {
            Debug.Log("Gate should be opening");
            foreach (Animator anim in animlist)
            {
                anim.SetBool("open", true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Vehicle2" || other.tag == "Vehicle3")
        {
            Debug.Log("Gate should be closing");
            foreach (Animator anim in animlist)
            {
                anim.SetBool("open", false);
            }
        }
    }
}
