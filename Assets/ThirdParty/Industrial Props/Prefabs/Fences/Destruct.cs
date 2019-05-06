using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruct : MonoBehaviour
{
    public MSSceneControllerFree _MSC;
    public GameObject destroyedObject;
    private BoxCollider myBoxCollider;

    void Start()
    {
        myBoxCollider.enabled = false;
    }

    void Awake()
    {
        myBoxCollider = GetComponent<BoxCollider>();
        _MSC = GameObject.Find("SceneController").GetComponent<MSSceneControllerFree>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vehicle2" || other.tag == "Vehicle3")
        {
            if (_MSC.vehicleCode.KMh > 30)
            {
                Debug.Log("fence hit and destroyed");
                Instantiate(destroyedObject, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("fence hit and not destroyed");
                myBoxCollider.enabled = true;
            }
        }
        else if (other.tag == "Player")
        {
            myBoxCollider.enabled = true;
        }
    }

    private void DisableCollider()
    {
        myBoxCollider.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Vehicle2" || other.tag == "Vehicle3" || other.tag == "Player")
        {
            Invoke("DisableCollider", 2);
        }
    }

}


