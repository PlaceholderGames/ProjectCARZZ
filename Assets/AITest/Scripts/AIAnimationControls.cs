using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimationControls : MonoBehaviour {

    public Animator anim;
    private AIBehaviour aIBehaiour;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        aIBehaiour = FindObjectOfType<AIBehaviour>();
    }
	
	// Update is called once per frame
	void Update () {
	    if(aIBehaiour.moveSpeed != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }



	}
}
