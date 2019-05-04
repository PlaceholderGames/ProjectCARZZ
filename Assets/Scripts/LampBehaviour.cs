using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampBehaviour : MonoBehaviour {

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Prop" || other.tag == "Vehicle3" || other.tag == "Vehicle2")
        {
            Light lightGO = GetComponentInChildren<Light>();
            lightGO.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Prop" || other.tag == "Vehicle3" || other.tag == "Vehicle2")
        {
            Light lightGO = GetComponentInChildren<Light>();
            lightGO.enabled = false;
        }
    }
}
