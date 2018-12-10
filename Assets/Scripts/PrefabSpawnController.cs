using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawnController : MonoBehaviour {
    public Terrain terrain;
    private int maxNumCol;//Maximum number of prefabs of the Collectable type
    private int maxNumAi;//Maximum number of prefabs of the Ai type
    private int terrainWidth;
    private int terrainLength;
    private int terrainPosX;
    private int terrainPosY;
    private int terrainPosZ;
    private bool isSpawning;
    private int numCol;//Number of prefabs in scene that are collectables
    private int numAi;//Number of prefabs in scene that are AI
    private int spawnTimeCol;
    private int spawnTimeAi;
    private GameObject[] moneySmall;
    public GameObject[] prefabCol;

    public void SpawnPrefab()
    {
        moneySmall = GameObject.FindGameObjectsWithTag("moneySmall");
        for (int h = 0; h < prefabCol.Length; h++)//for each prefab within moneySmall
        {
            int posX = Random.Range(terrainPosX, terrainPosX + terrainWidth);
            int posZ = Random.Range(terrainPosZ, terrainPosZ + terrainLength);
            float posY = Terrain.activeTerrain.SampleHeight(new Vector3(posX, 0, posZ));
            for (int j = 0; j < maxNumCol; j++)
            {
                if (prefabCol[h].transform.position.x == posX && prefabCol[h].transform.position.z == posZ)
                {
                    prefabCol[h] = Instantiate(prefabCol[h], new Vector3(Random.Range(terrainPosX, terrainPosX + terrainWidth), posY+0.5f, Random.Range(terrainPosZ, terrainPosZ + terrainLength)), Quaternion.identity);
                }
                prefabCol[h] = Instantiate(prefabCol[h], new Vector3(posX, posY+0.5f, posZ), Quaternion.identity);
                numCol++;
            }
        }
    }

    public void StartUp(int maxAi, int maxCol, int nAi, int nCol, int spawnTCol, int spawnTAi, bool spawning)
    {
        maxNumAi = maxAi;
        maxNumCol = maxCol;
        numAi = nAi;
        numCol = nCol;
        spawnTimeCol = spawnTCol;
        spawnTimeAi = spawnTAi;
        isSpawning = spawning;
        terrainLength = (int)terrain.terrainData.size.x;
        terrainWidth = (int)terrain.terrainData.size.z;
        terrainPosX = (int)terrain.transform.position.x;
        terrainPosY = (int)terrain.transform.position.y;
        terrainPosZ = (int)terrain.transform.position.z;
        moneySmall = GameObject.FindGameObjectsWithTag("moneySmall");
    }

	// Use this for initialization
	void Start () {
        StartUp(10, 100, 0, 0, 1, 1, false);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!isSpawning && numCol < maxNumCol)
        {
            Invoke("SpawnPrefab", spawnTimeCol);
            isSpawning = true;
        }
    }
}
