using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour {

    public Transform player;
    private MSSceneControllerFree sceneController;

    private void Start()
    {
        sceneController = FindObjectOfType<MSSceneControllerFree>();
    }

    private void LateUpdate()
    {
        if(sceneController.vehicleCode.isInsideTheCar)
        {
            Vector3 newPos = sceneController.vehicleCode.transform.position;
            newPos.y = transform.position.y;
            transform.position = newPos;

            transform.rotation = Quaternion.Euler(90.0f, sceneController.vehicleCode.transform.eulerAngles.y, 0.0f);
        }
        else
        {
            Vector3 newPos = player.transform.position;
            newPos.y = transform.position.y;
            transform.position = newPos;

            transform.rotation = Quaternion.Euler(90.0f, player.transform.eulerAngles.y, 0.0f);
        }
    }
}
