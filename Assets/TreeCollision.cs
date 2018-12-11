using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCollision : MonoBehaviour {

    public bool gotHit;
	// Use this for initialization
	void Start () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("TreeHit!");
        if (collision.collider.tag == "Vehicle2" || collision.collider.tag == "Vehicle3")
        {
            Debug.Log("TreeHit!");
            gotHit = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Vehicle2" || collision.collider.tag == "Vehicle3")
        {
            gotHit = false;
        }
    }
}
