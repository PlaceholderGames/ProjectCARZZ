using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSystem : MonoBehaviour {

    public MSSceneControllerFree _MSC;

    void Awake()
    {
        _MSC = GameObject.Find("SceneController").GetComponent<MSSceneControllerFree>();
    }

    public void OnTriggerEnter()
    {
        Destroy(gameObject);
        Debug.Log("Repair kit picked up");
        _MSC.repairValue++;

    }
    void Update()
    {
        if (Time.timeScale == 1.0f)
        {
            gameObject.transform.Rotate(0, 1.0f, 0, Space.World);
        }
    }
}
