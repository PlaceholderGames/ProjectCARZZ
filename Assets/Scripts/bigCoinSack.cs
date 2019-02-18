using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigCoinSack : MonoBehaviour
{

    private MSSceneControllerFree _MSC;
    [SerializeField] private float rotationSpeed = 100.0f;

    public int valueOfSack;

    void Awake()
    {
        _MSC = FindObjectOfType<MSSceneControllerFree>();
    }

    public void OnTriggerEnter()
    {
        Destroy(gameObject);
        Debug.Log("Player");
        _MSC.coinValue = _MSC.coinValue + valueOfSack;
    }

    void Update()
    {
        gameObject.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
