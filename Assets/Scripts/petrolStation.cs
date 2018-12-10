using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class petrolStation : MonoBehaviour {

<<<<<<< HEAD
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

 


=======
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
>>>>>>> 9698ae62a8218527137fff031d6ddfc5d3b57d70
}
