using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class petrolStation : MonoBehaviour {


    public MSSceneControllerFree _MSC;
    bool isInside;

    void Awake()
    {
        _MSC = GameObject.Find("SceneController").GetComponent<MSSceneControllerFree>();
    }

    void OnTriggerEnter(Collider Player)
    {
        isInside = true;
        _MSC.insidePetrolStation = isInside;
    }
    void OnTriggerExit(Collider other)
    {
        isInside = false;
        _MSC.insidePetrolStation = isInside;
    }





    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (_MSC.insidePetrolStation)
        {
            Debug.Log("Inside Petrol Station");

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (_MSC.coinValue >= 5)
                {
                    Debug.Log("Buying Petrol");
                    _MSC.coinValue = _MSC.coinValue - 5;
                    _MSC.fuelValue++;
                }
            }
        }
    }
}
