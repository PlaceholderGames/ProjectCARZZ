using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class petrolStation : MonoBehaviour {

    public MSSceneControllerFree _MSC;

    void Awake()
    {
        _MSC = GameObject.Find("SceneController").GetComponent<MSSceneControllerFree>();
    }

    void OnTriggerStay(Collider Player)
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
