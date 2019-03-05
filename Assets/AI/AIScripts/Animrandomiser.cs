using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animrandomiser : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Animator anim = GetComponentInChildren<Animator>();
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);//could replace 0 by any other animation layer index
        anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
    }
	

}
