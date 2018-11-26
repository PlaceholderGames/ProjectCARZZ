using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour
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
        Debug.Log("picked up coin sack of value " + valueOfSack);
        _MSC.coinValue = _MSC.coinValue + valueOfSack;
    }

    void Update()
    {
        if (Time.timeScale == 1.0f)
        {
            gameObject.transform.Rotate(0, 1f, 0, Space.World);
        }
    }
}
