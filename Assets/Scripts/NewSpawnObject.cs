using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnObject : MonoBehaviour {
    public Terrain terrain;
    public int currentObjects;//current amount of objects in scene
    public int CurrentNumberAi = 0;//number of ai at current time
    public int maxNumberObjects;//max number of objects in scene
    public int MaxNumberAi = 5;//max amount of ai at a time
    public float despawnTime;//time before ai is killed
    public float SpawnIntervalAi = 5;//time it takes for new ai to spawn
    public float SpawnIntervalcollectables = 5;//time it takes for new ai to spawn

    public int DetectDistanceAi = 50;
    public GameObject[] objectToPlace;
    private GameObject[] moneySmall;
    private GameObject[] moneyMedium;
    private GameObject[] moneyLarge;
    private GameObject[] repair;
    private GameObject[] ai;

    private int terrainWidth;
    private int terrainLength;
    private int terrainPosX;
    private int terrainPosY;
    private bool isSpawning;
    
    private AIBehaviour[] aIBehaiour;
    private AICollision[] aICollision;
    private MSVehicleControllerFree vehicle;
    private MSFPSControllerFree player;
    private Transform tempTransform;//used for switiching between player and vehicle
    private Transform vehicleTransform;//vehicle in current scene
    private Transform playerTransform;//player in current scene

    
    private float moveSpeed = 0.015f;//speed at which ai move

    private void Instantiation()
    {
        terrainWidth = (int)terrain.terrainData.size.x;
        terrainLength = (int)terrain.terrainData.size.z;
        terrainPosX = (int)terrain.transform.position.x;
        terrainPosY = (int)terrain.transform.position.z;

        moneySmall = GameObject.FindGameObjectsWithTag("moneySmall");
        //moneyMedium = GameObject.FindGameObjectsWithTag("moneyMedium");
        //moneyLarge = GameObject.FindGameObjectsWithTag("moneyLarge");
        //repair = GameObject.FindGameObjectsWithTag("repair");
        //ai = GameObject.FindGameObjectsWithTag("ai");

        //aIBehaiour = FindObjectsOfType<AIBehaviour>();
        //aICollision = FindObjectsOfType<AICollision>();
        //vehicle = FindObjectOfType<MSVehicleControllerFree>();
        //player = FindObjectOfType<MSFPSControllerFree>();
        //vehicleTransform = vehicle.transform;

        isSpawning = false;
    }

    void Start () {
        Instantiation();
    }
    
    private void ObjectPrefab(GameObject[] obj, int x , int y, int i)
    {
        for (int c = 0; c < obj.Length; c++)
        {
            if (obj[i].transform.position.x == x && obj[i].transform.position.z == y)
            {
                x = Random.Range(terrainPosX, terrainPosX + terrainWidth);
                y = Random.Range(terrainPosY, terrainPosY + terrainLength);
            }
        }
    }
    private void SpawnAI()
    {

    }

    public void SpawnObject()
    {
        int posx = Random.Range(terrainPosX, terrainPosX + terrainWidth);
        int posz = Random.Range(terrainPosY, terrainPosY + terrainLength);
        float posy = Terrain.activeTerrain.SampleHeight(new Vector3(posx, 0, posz));

        moneySmall = GameObject.FindGameObjectsWithTag("moneySmall");
        //moneyMedium = GameObject.FindGameObjectsWithTag("moneyMedium");
        //moneyLarge = GameObject.FindGameObjectsWithTag("moneyLarge");
        //repair = GameObject.FindGameObjectsWithTag("repair");
        //ai = GameObject.FindGameObjectsWithTag("ai");

        

        for (int i = 0; i < objectToPlace.Length; i++)
        {
            for(int l = 0; l < maxNumberObjects; l++)
            {
                GameObject newObject = (GameObject)Instantiate(objectToPlace[i], new Vector3(posx, posy + 1, posz), Quaternion.identity);
                ObjectPrefab(moneySmall, posx, posz, i);
                currentObjects++;
            }
            
            //ObjectPrefab(moneyMedium, posx, posz, i);
            //ObjectPrefab(moneyLarge, posx, posz, i);
            //ObjectPrefab(repair, posx, posz, i);
            

            //ObjectPrefab(ai, posx, posz, i);
            //CurrentNumberAi++;
            
            //aIBehaiour = FindObjectsOfType<AIBehaviour>();
            //aICollision = FindObjectsOfType<AICollision>();

            //if (vehicle.isInsideTheCar == false)
            //    playerTransform = player.transform;
            //else
            //{
            //    tempTransform = playerTransform;
            //    playerTransform = vehicleTransform;
            //}

            //for (int k = 0; k < aIBehaiour.Length; k++)
            //{
            //    aIBehaiour[k].target = playerTransform;
            //    aIBehaiour[k].moveSpeed = moveSpeed;
            //    aIBehaiour[k].detectDistance = DetectDistanceAi;
            //}

            //for (int k = 0; k < aICollision.Length; k++)
            //    aICollision[k].despawnTime = despawnTime;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isSpawning && currentObjects < maxNumberObjects)
        {
            Invoke("SpawnObject", SpawnIntervalcollectables);
            isSpawning = true;
        }
    }

}
