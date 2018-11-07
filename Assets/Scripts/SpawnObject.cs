using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public GameObject EnemyPrefab;
    
    public Transform target;
    public Vector3 center;
    public Vector3 size;
    public Color col = new Color(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1), 0.5f);
    
    public int aiKillTimer;
    public int aiNumber = 0;
    public int aiMax = 5;
    public float aiSpawnTimer = 5;
    public float aiSpeed;

    private AIBehaviour[] aIBehaiour;
    private AICollision[] aICollision;
    private float []aiEnemy;
    private bool isSpawning;

    public void SpawnZombie()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), -25, Random.Range(-size.z / 2, size.z / 2));
        Instantiate(EnemyPrefab, pos, Quaternion.identity);
        isSpawning = false;
        aiNumber++;
        aiSpeed = Random.Range(0.01f, 0.1f);//come back to this it changes all the ai as a whole and not individually
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = col;
        Gizmos.DrawCube(center, size);
    }

    void Awake()
    {
        isSpawning = false;
    }

    // Use this for initialization
    void Start () {
        aIBehaiour = FindObjectsOfType<AIBehaviour>();
        aICollision = FindObjectsOfType<AICollision>();
    }

    // Update is called once per frame
    void Update() {
        aIBehaiour = FindObjectsOfType<AIBehaviour>();
        aICollision = FindObjectsOfType<AICollision>();
        
        if (!isSpawning && aiNumber < aiMax)
        {
            Invoke("SpawnZombie", aiSpawnTimer);
            isSpawning = true;
        }

        for (int i = 0; i < aIBehaiour.Length; i++)
        {
            aIBehaiour[i].target = target;
            aIBehaiour[i].moveSpeed = aiSpeed;
        }
        
        for (int i = 0; i < aICollision.Length; i++)
        {
            aICollision[i].aiKillTimer = aiKillTimer;
        }
    }

}
