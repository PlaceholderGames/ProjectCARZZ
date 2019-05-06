using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampBehaviour : MonoBehaviour {

    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit light");
        if (other.tag == "Vehicle3" || other.tag == "Vehicle2")
        {
            Light lightGO = GetComponentInChildren<Light>();
            lightGO.enabled = false;
        }
    }
}
