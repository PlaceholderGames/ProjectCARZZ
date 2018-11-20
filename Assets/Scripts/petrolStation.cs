using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class petrolStation : MonoBehaviour {

    public MSSceneControllerFree _MSC;
    

    void Awake()
    {
        _MSC = GameObject.Find("SceneController").GetComponent<MSSceneControllerFree>();
        _MSC.insidePetrolStation = false;
    }

    void OnTriggerStay(Collider Player)
    {
        _MSC.insidePetrolStation = true;

        Debug.Log("Inside Petrol Station");

        if (Input.GetKeyDown(KeyCode.Return) && _MSC.player.activeSelf)
        {
            if (_MSC.coinValue >= 5 && _MSC.fuelSlider.value == 100)
            {
                Debug.Log("Buying Petrol for the can");
                _MSC.coinValue -= 5;
                _MSC.fuelValue += 1;
            }
            else
            {
                Debug.Log("Buying Petrol for the car");
                _MSC.coinValue -= 5;
                _MSC.fuelSlider.value = 100;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && _MSC.player.activeSelf && _MSC.coinValue >= 5 && _MSC.fuelSlider.value < 100)
        {
            Debug.Log("Buying Petrol for the car");
            _MSC.coinValue -= 5;
            _MSC.fuelSlider.value = 100;
        }


    }


    private void OnTriggerExit(Collider Player)
    {
        _MSC.insidePetrolStation = false;
    }


}
