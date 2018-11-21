using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnObject : MonoBehaviour {
    public Terrain terrain;
    public int currentObjects;//current amount of objects in scene
    public int maxNumberObjects;//max number of objects in scene
    public int SpawnIntervalAi;
    public GameObject[] objectToPlace;
    private CoinSystem[] coin;
    private FuelSystem[] gas;
    private RepairSystem[] repair;
    private int terrainWidth;
    private int terrainLength;
    private int terrainPosX;
    private int terrainPosY;
    private bool isSpawning;
    
    void Start () {
        terrainWidth = (int)terrain.terrainData.size.x;
        terrainLength = (int)terrain.terrainData.size.z;
        terrainPosX = (int)terrain.transform.position.x;
        terrainPosY = (int)terrain.transform.position.z;
        coin = FindObjectsOfType<CoinSystem>();
        gas = FindObjectsOfType<FuelSystem>();
        repair = FindObjectsOfType<RepairSystem>();
        isSpawning = false;
    }

    public void SpawnObject()
    {
        coin = FindObjectsOfType<CoinSystem>();
        gas = FindObjectsOfType<FuelSystem>();
        repair = FindObjectsOfType<RepairSystem>();

        int posx = Random.Range(terrainPosX, terrainPosX + terrainWidth);
        int posz = Random.Range(terrainPosY, terrainPosY + terrainLength);
        float posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, 0, posz));
        posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, 0, posz));


        for (int i = 0; i < objectToPlace.Length; i++)
        {
            GameObject newObject = (GameObject)Instantiate(objectToPlace[i], new Vector3(posx, posy + 1, posz), Quaternion.identity);
            for(int c = 0; c < coin.Length; c++)
            {
                if (coin[i].transform.position.x == posx && coin[i].transform.position.z == posz)
                {
                    posx = Random.Range(terrainPosX, terrainPosX + terrainWidth);
                    posz = Random.Range(terrainPosY, terrainPosY + terrainLength);
                }
            }
            for (int g = 0; g < gas.Length; g++)
            {
                if (gas[i].transform.position.x == posx && gas[i].transform.position.z == posz)
                {
                    posx = Random.Range(terrainPosX, terrainPosX + terrainWidth);
                    posz = Random.Range(terrainPosY, terrainPosY + terrainLength);
                }
            }
            for (int r = 0; r < repair.Length; r++)
            {
                if (repair[i].transform.position.x == posx && repair[i].transform.position.z == posz)
                {
                    posx = Random.Range(terrainPosX, terrainPosX + terrainWidth);
                    posz = Random.Range(terrainPosY, terrainPosY + terrainLength);
                }
            }

            isSpawning = false;
            currentObjects++;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isSpawning && currentObjects < maxNumberObjects)
        {
            Invoke("SpawnObject", SpawnIntervalAi);
            isSpawning = true;
        }
    }

}
