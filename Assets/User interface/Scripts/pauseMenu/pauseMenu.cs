using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public MSSceneControllerFree _MSC;

    public GameObject mainCanvas;
    public GameObject gameSubMenu;
    public GameObject videoSubMenu;
    public GameObject audioSubMenu;
    public GameObject optionsMenu;
    public GameObject saveSubMenu;


    void OnEnable()
    {
        if (gameSubMenu == true || videoSubMenu == true || audioSubMenu == true || optionsMenu == true || saveSubMenu == true)
        {
            KillThosePanels();
        }
    }

    void KillThosePanels()
    {
        mainCanvas.SetActive(true);
        gameSubMenu.SetActive(false);
        videoSubMenu.SetActive(false);
        audioSubMenu.SetActive(false);
        optionsMenu.SetActive(false);
        saveSubMenu.SetActive(false);

    }

}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class pauseMenu : MonoBehaviour {

//    public MSSceneControllerFree _MSC;

//    public static bool gamePaused = false;

//    public GameObject pauseMenuUI;

//	// Update is called once per frame
//	void Update () {
//		if (Input.GetKeyDown(KeyCode.Escape))
//        {
//            if (gamePaused)
//            {
//                Resume();
//            }
//            else
//            {
//                Pause();
//            }
//        }
//	}

//    public void Resume()
//    {
//        gamePaused = false;
//        Time.timeScale = 1f;
//        Cursor.lockState = CursorLockMode.None;
//        Cursor.visible = false;
//        pauseMenuUI.SetActive(false);
//    }
//    void Pause()
//    {
//        Time.timeScale = 0f;
//        gamePaused = true;
//        Cursor.lockState = CursorLockMode.Locked;
//        Cursor.visible = true;
//        pauseMenuUI.SetActive(true);
//    }

//    public void quitToMenu()
//    {
//        Cursor.lockState = CursorLockMode.None;
//        Cursor.visible = true;
//        Time.timeScale = 1f;
//    }

//}
