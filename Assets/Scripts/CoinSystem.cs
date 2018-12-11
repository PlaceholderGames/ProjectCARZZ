using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSystem : MonoBehaviour {

    public MSSceneControllerFree _MSC;

    void Awake()
    {
        _MSC = GameObject.Find("SceneController").GetComponent<MSSceneControllerFree>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            Destroy(gameObject);
            Debug.Log("Player");
            _MSC.coinValue++;
        }
    }

    void Update()
    {
        gameObject.transform.Rotate(0, 2.5f, 0, Space.World);
    }
}
