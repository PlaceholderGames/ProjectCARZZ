using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSystem : MonoBehaviour {

    private MSSceneControllerFree _MSC;
    private NewSpawnObject nso;
    [SerializeField] private float rotationSpeed = 100.0f;

    void Awake()
    {
        _MSC = FindObjectOfType<MSSceneControllerFree>();
        nso = FindObjectOfType<NewSpawnObject>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player" || other.GetComponent<Collider>().tag == "Vehicle2" || other.GetComponent<Collider>().tag == "Vehicle3")
        {
            Destroy(gameObject);
            _MSC.repairValue++;
            nso.currentObjects--;
        }

    }
    void Update()
    {
        gameObject.transform.Rotate(0, rotationSpeed*Time.deltaTime, 0, Space.World);
    }
}
