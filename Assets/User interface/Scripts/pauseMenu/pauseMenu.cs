using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour {

    public MSSceneControllerFree _MSC;

    public static bool gamePaused = false;

    public GameObject pauseMenuUI;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Resume()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
    }
    void Pause()
    {
        Time.timeScale = 0f;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
    }

    public void quitToMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
    }

}
