using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public GameObject EnemyPrefab;
    public GameObject player;
    public AIBehaviour []aIBehaiour;
    public Transform target;

    public Vector3 center;
    public Vector3 size;
    public Color col = new Color(0,1,0,0.5f);
    

    public int aiNumber = 0;
    public int aiMax = 5;
    public float aiSpawnTimer = 5;

    private bool isSpawning;

    void Awake()
    {
        isSpawning = false;
    }

    public void SpawnZombie()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), -25, Random.Range(-size.z / 2, size.z / 2));
        Instantiate(EnemyPrefab, pos, Quaternion.identity);
        
        isSpawning = false;
    }
    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = col;
        Gizmos.DrawCube(center, size);
    }
    
    // Use this for initialization
    void Start () {
        aIBehaiour = FindObjectsOfType<AIBehaviour>();
    }

    // Update is called once per frame
    void Update() {
        aIBehaiour = FindObjectsOfType<AIBehaviour>();
        //do for loop for list
        if (!isSpawning && aiNumber < aiMax)
        {
            aiNumber++;
            Invoke("SpawnZombie", aiSpawnTimer);
            isSpawning = true;
        }
    }

}
