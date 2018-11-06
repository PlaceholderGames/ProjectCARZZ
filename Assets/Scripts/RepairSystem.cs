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
        Debug.Log("HIT");
        _MSC.repairValue++;

    }
    void Update()
    {
        gameObject.transform.Rotate(0, 2.5f, 0, Space.World);
    }
}
