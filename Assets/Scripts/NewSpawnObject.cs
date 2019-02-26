using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnObject : MonoBehaviour {
    public Terrain terrain;
    public int currentObjects;//current amount of objects in scene
    public int maxNumberObjects;//max number of objects in scene
    public float despawnTime;//time before ai is killed
    public float SpawnIntervalcollectables = 5;//time it takes for new ai to spawn

    public GameObject[] Collectables;

    private int terrainWidth;
    private int terrainLength;
    private int terrainPosX;
    private int terrainPosY;
    private bool isSpawning;
    
    private Transform tempTransform;//used for switiching between player and vehicle
    private Transform vehicleTransform;//vehicle in current scene
    private Transform playerTransform;//player in current scene
	int posx = 0;
	int posz = 0;
    
    private float moveSpeed = 0.015f;//speed at which ai move

    private void Instantiation()
    {
        terrainWidth = (int)terrain.terrainData.size.x;
        terrainLength = (int)terrain.terrainData.size.z;
        terrainPosX = (int)terrain.transform.position.x;
        terrainPosY = (int)terrain.transform.position.z;
        //moneyMedium = GameObject.FindGameObjectsWithTag("moneyMedium");
        //moneyLarge = GameObject.FindGameObjectsWithTag("moneyLarge");
        //repair = GameObject.FindGameObjectsWithTag("repair");
        //ai = GameObject.FindGameObjectsWithTag("ai");

        //aIBehaiour = FindObjectsOfType<AIBehaviour>();
        //aICollision = FindObjectsOfType<AICollision>();
        //vehicle = FindObjectOfType<MSVehicleControllerFree>();
        //player = FindObjectOfType<MSFPSControllerFree>();
        //vehicleTransform = vehicle.transform;

		posx = Random.Range(terrainPosX, terrainPosX + terrainWidth);
        posz = Random.Range(terrainPosY, terrainPosY + terrainLength);
    }

    void Start () {
        Instantiation();
    }

    private void SpawnObject()
    {
        posx = Random.Range(terrainPosX, terrainPosX + terrainWidth);
		posz = Random.Range(terrainPosY, terrainPosY + terrainLength);
        float posy = terrain.SampleHeight(new Vector3(posx, 0, posz));
        //moneyMedium = GameObject.FindGameObjectsWithTag("moneyMedium");
        //moneyLarge = GameObject.FindGameObjectsWithTag("moneyLarge");
        //repair = GameObject.FindGameObjectsWithTag("repair");
        //ai = GameObject.FindGameObjectsWithTag("ai");
		int j = Random.Range(0, Collectables.Length);
        Instantiate(Collectables[j], new Vector3(posx, posy + 1, posz), Quaternion.Euler(-90.0f, 0, 0));
        currentObjects++;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (currentObjects < maxNumberObjects)
        {
            StartCoroutine("SpawnObject");
        }
    }

}
