using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectables : MonoBehaviour
{

    public GameObject[] Prefab;
    public Vector3 center;//visual for seeing spawn area
    public Vector3 size;//size of spawn area
    private Color col = new Color(1, 0, 0, 0.5f);
    public int collectablesMax = 5;
    public float collectablesSpawnTimer = 5;
    public int collectableNumber = 0;

    private bool isSpawning;

    public void SpawnCollectable()
    {
        for (int i = 0; i < Prefab.Length; i++)
        {
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), -25, Random.Range(-size.z / 2, size.z / 2));
            Instantiate(Prefab[i], pos, Quaternion.identity);
            collectableNumber++;
            isSpawning = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = col;
        Gizmos.DrawCube(center, size);
    }

    // Use this for initialization
    void Start(){}

    // Update is called once per frame
    void Update()
    {

        if (!isSpawning && collectableNumber < collectablesMax)
        {
            Invoke("SpawnCollectable", collectablesSpawnTimer);
            isSpawning = true;
        }
    }

}
