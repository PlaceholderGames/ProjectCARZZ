using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelSystem : MonoBehaviour {
    public MSSceneControllerFree _MSC;

    void Awake()
    {
        _MSC = GameObject.Find("SceneController").GetComponent<MSSceneControllerFree>();
    }


    public void OnTriggerEnter()
    {
        Destroy(gameObject);
        Debug.Log("Picked up Fuel Can");
        _MSC.fuelValue++;
    }

    void Update()
    {
        if (Time.timeScale == 1.0f)
        {
            gameObject.transform.Rotate(0, 2.5f, 0, Space.World);
        }
    }

}

