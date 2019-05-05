using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public Terrain terrain;
    public GameObject SpawnPrefab;//Object to spawn
    public float radius;
    public int MaxNumberAi = 5;//max amount of ai at a time
    public float SpawnIntervalAi = 5;//time it takes for new ai to spawn
    public int CurrentNumberAi = 0;//number of ai at current time
    public List<GameObject> PooledObjects;

    private Color col = new Color(1, 0, 0, 0.5f);
    private MSVehicleControllerFree vehicle;
    private Transform vehicleTransform;//vehicle in current scene

	private float distanceV = 0;
    private TerrainItemSpawner terrIt;
    private Vector3 center;//visual for seeing spawn area


    public void SpawnZombie()
    {
        CurrentNumberAi++;
        StartCoroutine(Spawn(1, 0, SpawnPrefab));

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = col;
        Gizmos.DrawSphere(transform.position, radius);
    }


    // Use this for initialization
    void Start () {
        vehicle = FindObjectOfType<MSVehicleControllerFree>();
        terrIt = FindObjectOfType<TerrainItemSpawner>();
        vehicleTransform = vehicle.transform;
        center = transform.position;
        center.y = terrain.SampleHeight(center);
        for(int i = CurrentNumberAi; i <= MaxNumberAi; i++)
        {
            SpawnZombie();
        }
    }

    // Update is called once per frame
    void Update()
	{
        
       // if ((distanceV < radius+100) && CurrentNumberAi < MaxNumberAi) // && (int)Time.time % (int)SpawnIntervalAi == 0
        //{
            
        //}
        foreach (GameObject obj in PooledObjects)
        {
            distanceV = Vector3.Distance(vehicle.transform.position, obj.transform.position);
            if (distanceV < radius)
            {
                AICollision aic = obj.GetComponent<AICollision>();
                if (!aic.dead)
                    obj.SetActive(true);
                
            }
        }
	}

    //Spawns an object randomly around terrain
    public IEnumerator Spawn(int amount, float timer, GameObject obj)
    {
        float timeToWait = Time.timeSinceLevelLoad + timer;
        while (Time.timeSinceLevelLoad < timeToWait)
        {
            yield return null;
        }

        float terrZ = terrain.terrainData.size.z; //gets terrain Z
        float terrX = terrain.terrainData.size.x; //gets terrain X
        for (int i = 0; i < amount; i++)
        {
            obj.SetActive(false);
            GameObject go = Instantiate(obj); //sets gameobject to instantiation of game object
            PooledObjects.Add(go);
            Vector3 pos = new Vector3(Random.Range(0, terrX), 0, Random.Range(0, terrZ)); //sets pos vector randomly
            pos.y = terrain.SampleHeight(pos) + 1.0f; //adjusts y accordingly
            go.transform.position = pos; //sets position to new pos
        }
    }

    //spawns object at positive inside circle of radius
    public IEnumerator Spawn(int amount, float timer, GameObject obj, Vector3 position, float radius)
    {
        float timeToWait = Time.timeSinceLevelLoad + timer;
        while (Time.timeSinceLevelLoad < timeToWait)
        {
            yield return null;
        }

        float terrZ = terrain.terrainData.size.z; //gets terrain Z
        float terrX = terrain.terrainData.size.x; //gets terrain X
        for (int i = 0; i < amount; i++)
        {
            obj.SetActive(false);
            GameObject go = Instantiate(obj); //sets gameobject to instantiation of game object
            PooledObjects.Add(go);
            Vector3 circle = (Random.insideUnitSphere * radius) + position; //gets random position within sphere and displaces it to the desired location
            circle.y = terrain.SampleHeight(circle) + 1.0f; //adjusts y accordingly
            go.transform.position = circle; //sets position to new pos
        }
    }

}
