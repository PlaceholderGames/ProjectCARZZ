using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class petrolStation : MonoBehaviour {

    public MSSceneControllerFree _MSC;
    

    void Awake()
    {
        _MSC = GameObject.Find("SceneController").GetComponent<MSSceneControllerFree>();
        _MSC.insidePetrolStation = false; // being lazy and made a public variable so we can use the pop up messages.
    }

    void OnTriggerStay(Collider Player)
    {
        _MSC.insidePetrolStation = true;

        if (Input.GetKeyDown(KeyCode.Return) && _MSC.player.activeSelf) // checking to see if the player is out of the vehicle
        {
            if (_MSC.coinValue >= 5 && _MSC.fuelSlider.value == 100) // checking if player has enough money and determining whether or not they have a full tank of petrol
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

    }


    private void OnTriggerExit(Collider Player)
    {
        _MSC.insidePetrolStation = false;
    }


}
