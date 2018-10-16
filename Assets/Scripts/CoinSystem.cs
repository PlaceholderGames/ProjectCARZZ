using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour {

    public MSSceneControllerFree _MSC;

    void Awake()
    {
        _MSC = GameObject.Find("SceneController").GetComponent<MSSceneControllerFree>();
    }

    public void OnTriggerEnter()
    {
        Destroy(gameObject);
        Debug.Log("HIT");
        _MSC.coinValue++;
    }
}
