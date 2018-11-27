using System.Collections;
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
    private MSSceneControllerFree sceneController;
    private GameObject activeVehicle;
    private MSFPSControllerFree player;
    private Transform tempTransform;//used for switiching between player and vehicle
    public Transform vehicleTransform;//vehicle in current scene
    private Transform playerTransform;//player in current scene
    

    private float []aiEnemy;//list of all ai in scene
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = col;
        Gizmos.DrawCube(center, size);
    }

    void Awake()
    {
        isSpawning = false;
    }

    void Start () {
        sceneController = GameObject.Find("SceneController").GetComponent<MSSceneControllerFree>();

        aICollision = FindObjectsOfType<AICollision>();
        player = FindObjectOfType<MSFPSControllerFree>();

        aIBehaiour = FindObjectsOfType<AIBehaviour>();

    }

    void Update()
    {
        activeVehicle = GameObject.FindGameObjectWithTag("activeVehicle");
        vehicleTransform = activeVehicle.transform;
    }
    // Update is called once per frame
    void FixedUpdate() {
        aIBehaiour = FindObjectsOfType<AIBehaviour>();
        aICollision = FindObjectsOfType<AICollision>();
        
        
        if (!isSpawning && CurrentNumberAi < MaxNumberAi)
        {
            Invoke("SpawnZombie", SpawnIntervalAi);
            isSpawning = true;
        }
        
        if (sceneController.vehicleCode.isInsideTheCar == false)
            playerTransform = player.transform;
        else
        {
            tempTransform = playerTransform;
            playerTransform = vehicleTransform;
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
