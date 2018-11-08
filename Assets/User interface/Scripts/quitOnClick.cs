using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitOnClick : MonoBehaviour {

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Will quit to the editor if playing in the editor
#else
        Application.Quit (); // If this is a build of the game, quit to desktop.
#endif
    }

}
