using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public Terrain terrain;
    public GameObject Prefab;//Object to spawn
    private Vector3 center;//visual for seeing spawn area
    public Vector3 size;//size of spawn area
    public float radius;

    public float despawnTime;//time before ai is killed
    public int MaxNumberAi = 5;//max amount of ai at a time
    public float SpawnIntervalAi = 5;//time it takes for new ai to spawn
    public int CurrentNumberAi = 0;//number of ai at current time
    public int DetectDistanceAi = 50;



    private Color col = new Color(1, 0, 0, 0.5f);
    private AIBehaviour[] aIBehaiour;
    private AIBehaviour aIBehaiour1;
    private AICollision[] aICollision;
    private AICollision aICollision1;
    private MSVehicleControllerFree vehicle;
    private MSFPSControllerFree player;
    private Transform tempTransform;//used for switiching between player and vehicle
    private Transform vehicleTransform;//vehicle in current scene
    private Transform playerTransform;//player in current scene
    

    private List<GameObject> aiEnemy = new List<GameObject>();//list of all ai in scene
    private bool isSpawning;
    //private bool isMoving;
    private float moveSpeed = 0.015f;//speed at which ai move
    private float runMoveSpeed = 0.05f;//speed at which ai run
	private float distanceV = 0;
    private TerrainItemSpawner terrIt;


    float x;
    Vector3 pos;
    float z;


    void SpawnZombieMap()
    {
		Debug.Log(SpawnIntervalAi);

        //Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), -25, Random.Range(-size.z / 2, size.z / 2));
        Instantiate(Prefab);
        aiEnemy.Add(Prefab);
        CurrentNumberAi++;
        aIBehaiour1 = Prefab.GetComponent<AIBehaviour>();
        aICollision1 = Prefab.GetComponent<AICollision>();
        aICollision1.despawnTime = despawnTime;
        aIBehaiour1.target = playerTransform;
        aIBehaiour1.moveSpeed = moveSpeed;
        aIBehaiour1.detectDistance = DetectDistanceAi;
        aIBehaiour1.killDistance = radius + 100;
        StartCoroutine(terrIt.Spawn(1, 1, Prefab));

    }

    public void SpawnZombie()
    {
        x = Random.Range(transform.position.x - radius, transform.position.x + radius);
        z = Random.Range(transform.position.z - radius, transform.position.z + radius);
        pos = new Vector3(x, 0, z);
        pos.y = terrain.SampleHeight(pos);

        //Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), -25, Random.Range(-size.z / 2, size.z / 2));
        //Instantiate(Prefab);
        CurrentNumberAi++;
        StartCoroutine(terrIt.Spawn(1, 0, Prefab, pos, radius));

    }
    public void DeSpawnZombie()
    {
        aICollision = FindObjectsOfType<AICollision>();
        for (int i = 0; i < aICollision.Length; i++)
        {
            Destroy(aICollision[i].gameObject);
        }
        if (aICollision.Length == 0)
            CurrentNumberAi = 0;

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = col;
        Gizmos.DrawSphere(transform.position, radius);
    }


    // Use this for initialization
    void Start () {
        aIBehaiour = FindObjectsOfType<AIBehaviour>();
        aICollision = FindObjectsOfType<AICollision>();
        vehicle = FindObjectOfType<MSVehicleControllerFree>();
        player = FindObjectOfType<MSFPSControllerFree>();
        terrIt = FindObjectOfType<TerrainItemSpawner>();
        vehicleTransform = vehicle.transform;
        center = transform.position;

        
    }

    // Update is called once per frame
    void Update()
	{
       
      //if (radius == 0)
            //{
            //    SpawnZombieMap();
            //}
            vehicle = FindObjectOfType<MSVehicleControllerFree>();
            vehicleTransform = vehicle.transform;
            center.y = terrain.SampleHeight(center);
            distanceV = Vector3.Distance(vehicle.transform.position, center);
            //if ((distanceV > radius+55)) DeSpawnZombie();
            if ((distanceV < radius+100) && CurrentNumberAi < MaxNumberAi) // && (int)Time.time % (int)SpawnIntervalAi == 0
            {
                SpawnZombie();
            }
            
            if(CurrentNumberAi < MaxNumberAi*2)
            {
                if ((distanceV < radius + 100) && (int)Time.time % (int)SpawnIntervalAi == 0)
                {
                    SpawnZombie();
                }
            }
        
		
	}

}
