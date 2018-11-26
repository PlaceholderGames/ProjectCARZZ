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


    void OnEnable() { 
        if (gameSubMenu == true || videoSubMenu == true || audioSubMenu == true || optionsMenu == true)
        {
            killThosePanels();
        }
    }

    void killThosePanels()
    {
        mainCanvas.SetActive(true);
        gameSubMenu.SetActive(false);
        videoSubMenu.SetActive(false);
        audioSubMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

}