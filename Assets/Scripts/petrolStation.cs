using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Audio;

public class petrolStation : MonoBehaviour
{
    public MSSceneControllerFree _MSC;

    void Awake()
    {
        _MSC = GameObject.Find("SceneController").GetComponent<MSSceneControllerFree>();
        _MSC.insidePetrolStation = false; // being lazy and made a public variable so we can use the pop up messages.
    }

    void OnTriggerStay(Collider Player)
    {
        if (Player.tag == "Player")
        {
            _MSC.insidePetrolStation = true;
        }

    }

    void Update()
    {
        if (_MSC.insidePetrolStation == true)
        {
            //if (Input.GetKeyDown(KeyCode.Return))
            //{
            //    Debug.Log("Trying to buy fuel for can");
            //    Debug.Log(_MSC.player.activeSelf);
            //    Debug.Log(_MSC.coinValue);
            //    Debug.Log(_MSC.fuelSlider.value);
            //}
            if (Input.GetKeyDown(KeyCode.Return) && _MSC.player.activeSelf && _MSC.coinValue >= 5 && _MSC.fuelSlider.value == _MSC.fuelSlider.maxValue) // checking to see if the player is out of the vehicle, checking if player has enough money and determining whether or not they have a full tank of petrol
            {
                _MSC.coinValue -= 5;
                Invoke("buyingCans", 0.5f);
            }
            if (Input.GetKeyDown(KeyCode.Return) && _MSC.player.activeSelf && _MSC.coinValue >= 5 && _MSC.fuelSlider.value < _MSC.fuelSlider.maxValue)
            {
                _MSC.coinValue -= 5;
                Invoke("buyingFuel", 0.5f);
            }

        }
    }

    void buyingFuel()
    {
        Debug.Log("Buying Petrol for the car");
        _MSC.fuelSlider.value = _MSC.fuelSlider.maxValue;
    }

    void buyingCans()
    {
        Debug.Log("Buying Petrol for the can");
        _MSC.fuelValue += 1;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _MSC.insidePetrolStation = false;
        }
    }


}
