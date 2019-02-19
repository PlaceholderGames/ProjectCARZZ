using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadSceneOnClick : MonoBehaviour {



	public void LoadByIndex(int sceneIndex)
    {
        PlayerPrefs.SetInt("isNewGame", 0);
        SceneManager.LoadScene(sceneIndex); // load whatever value was passed into the function
    }
}
