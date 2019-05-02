using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newSceneOnClick : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }
    public void LoadByIndex(int sceneIndex)
    {
        PlayerPrefs.SetInt("isNewGame", 1);
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadByName(string name)
    {
        PlayerPrefs.SetInt("isNewGame", 1);
        PlayerPrefs.SetString("SceneName", name);
        SceneManager.LoadScene(1);
    }

}
