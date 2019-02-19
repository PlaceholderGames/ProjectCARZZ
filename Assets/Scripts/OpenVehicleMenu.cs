using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OpenVehicleMenu : MonoBehaviour {
    // Use this for initialization

    public MSSceneControllerFree mSScene;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            mSScene.SavePP();
            PlayerPrefs.SetInt("isNewGame", 0);
            SceneManager.LoadScene("Garage");
        }
    }
 
}
