﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public GameObject Prefab;//Object to spawn
    public Vector3 center;//visual for seeing spawn area
    public Vector3 size;//size of spawn area

    public float despawnTime;//time before ai is killed
    public int MaxNumberAi = 5;//max amount of ai at a time
    public float SpawnIntervalAi = 5;//time it takes for new ai to spawn
    public int CurrentNumberAi = 0;//number of ai at current time
    public int DetectDistanceAi = 50;



    private Color col = new Color(1, 0, 0, 0.5f);
    private AIBehaviour[] aIBehaiour;
    private AICollision[] aICollision;
    private MSVehicleControllerFree vehicle;
    private MSFPSControllerFree player;
    private Transform tempTransform;//used for switiching between player and vehicle
    private Transform vehicleTransform;//vehicle in current scene
    private Transform playerTransform;//player in current scene
    

    private List<GameObject> aiEnemy;//list of all ai in scene
    private bool isSpawning;
    //private bool isMoving;
    private float moveSpeed = 0.015f;//speed at which ai move
    private float runMoveSpeed = 0.1f;//speed at which ai run

    public void SpawnZombie()
    {
            
            Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), -25, Random.Range(-size.z / 2, size.z / 2));
            Instantiate(Prefab, pos, Quaternion.identity);
            isSpawning = false;
            CurrentNumberAi++;
           
    }
    public void DeSpawnZombie()
    {
        for(int i = 0; i < aICollision.Length; i++)
        {
            if ((aICollision[i].transform.position.x > (center-size).x && aICollision[i].transform.position.x < (center + size).x) && (aICollision[i].transform.position.z > (center - size).z && aICollision[i].transform.position.z < (center + size).z))
            {
                isSpawning = false;
                Destroy(aICollision[i].gameObject);
                CurrentNumberAi--;
            }
        }
        
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
        vehicle = FindObjectOfType<MSVehicleControllerFree>();
        player = FindObjectOfType<MSFPSControllerFree>();
        vehicleTransform = vehicle.transform;
    }

    // Update is called once per frame
    void FixedUpdate() {
        aIBehaiour = FindObjectsOfType<AIBehaviour>();
        aICollision = FindObjectsOfType<AICollision>();
        vehicle = FindObjectOfType<MSVehicleControllerFree>();
        float distanceV = Vector3.Distance(vehicle.transform.position, center);
        
        if(!(distanceV < 300)) DeSpawnZombie();
        if (!isSpawning && CurrentNumberAi < MaxNumberAi && (distanceV < 300))
        {
            Invoke("SpawnZombie", SpawnIntervalAi);
            isSpawning = true;
        }
        
        
        if (vehicle.isInsideTheCar == false)
            playerTransform = player.transform;
        else{
            playerTransform = vehicle.transform;
        }

        for (int i = 0; i < aIBehaiour.Length; i++)
        {
            aIBehaiour[i].target = playerTransform;
            aIBehaiour[i].moveSpeed = moveSpeed;
            aIBehaiour[i].detectDistance = DetectDistanceAi;
        }
        
        for (int i = 0; i < aICollision.Length; i++)
            aICollision[i].despawnTime = despawnTime;
    }

}
