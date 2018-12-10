using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newSceneOnClick : MonoBehaviour
{

    public void LoadByIndex(int sceneIndex)
    {
        PlayerPrefs.SetInt("isNewGame", 1);
        SceneManager.LoadScene(sceneIndex);
    }

}
