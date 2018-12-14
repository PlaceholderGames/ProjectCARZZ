using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OpenVehicleMenu : MonoBehaviour {
    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerPrefs.SetInt("isNewGame", 0);
            SceneManager.LoadScene("Garage");
        }
    }
 
}
