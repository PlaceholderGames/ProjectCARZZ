using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigCoinSack : MonoBehaviour
{

    public MSSceneControllerFree _MSC;

    public int valueOfSack;

    void Awake()
    {
        _MSC = GameObject.Find("SceneController").GetComponent<MSSceneControllerFree>();
    }

    public void OnTriggerEnter()
    {
        Destroy(gameObject);
        Debug.Log("Player");
        _MSC.coinValue = _MSC.coinValue + valueOfSack;
    }

    void Update()
    {
        gameObject.transform.Rotate(0, 2.5f, 0, Space.World);
    }
}
