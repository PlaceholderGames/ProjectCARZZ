using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainItemSpawner : MonoBehaviour
{
    [Header("Terrain Item Spawning script will spawn selected items randomly.")]
    [SerializeField]
    [Tooltip("List of the desired objects you want to spawn.")]
    List<GameObject> objectsToSpawn = new List<GameObject>();

    [SerializeField]
    [Tooltip("Sets the standard number of objects to spawn")]
    int numberOfObjectsToSpawn = 10;

    //public Transform trans;

    Terrain terr;
    TerrainData terrDat;
    // Start is called before the first frame update
    void Start()
    {
        terr = GetComponent<Terrain>(); //gets Terrain component on current GameObject
        terrDat = terr.terrainData; //gets the terraindata from the above terrain component
        //StartCoroutine(Spawn(numberOfObjectsToSpawn, 2.0f, objectsToSpawn, trans.position, 20.0f)); //comment this bad boi out to stop this script from spawning stuff by default
    }

    //spawns random objects from list randomly around terrain
    public IEnumerator Spawn(int amount, float timer, List<GameObject> list)
    {
        float timeToWait = Time.timeSinceLevelLoad + timer;
        while(Time.timeSinceLevelLoad < timeToWait)
        {
            yield return null;
        }

        float terrZ = terrDat.size.z; //gets terrain Z
        float terrX = terrDat.size.x; //gets terrain X
        for (int i = 0; i < amount; i++)
        {
            GameObject go = Instantiate(list[Random.Range(0, list.Count)]); //sets gameobject to instantiation of game object
            Vector3 pos = new Vector3(Random.Range(0, terrX), 0, Random.Range(0, terrZ)); //sets pos vector randomly
            pos.y = terr.SampleHeight(pos) + 1.0f; //adjusts y accordingly
            go.transform.position = pos; //sets position to new pos
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

        float terrZ = terrDat.size.z; //gets terrain Z
        float terrX = terrDat.size.x; //gets terrain X
        for (int i = 0; i < amount; i++)
        {
            GameObject go = Instantiate(obj); //sets gameobject to instantiation of game object
            //objectsToSpawn.Add(go);
            Vector3 pos = new Vector3(Random.Range(0, terrX), 0, Random.Range(0, terrZ)); //sets pos vector randomly
            pos.y = terr.SampleHeight(pos) + 1.0f; //adjusts y accordingly
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

        float terrZ = terrDat.size.z; //gets terrain Z
        float terrX = terrDat.size.x; //gets terrain X
        for (int i = 0; i < amount; i++)
        {
            GameObject go = Instantiate(obj); //sets gameobject to instantiation of game object
            objectsToSpawn.Add(go);
            Vector3 circle = (Random.insideUnitSphere * radius) + position; //gets random position within sphere and displaces it to the desired location
            circle.y = terr.SampleHeight(circle) + 1.0f; //adjusts y accordingly
            go.transform.position = circle; //sets position to new pos
        }
    }

    //random object at position inside circle of radius
    public IEnumerator Spawn(int amount, float timer, List<GameObject> list, Vector3 position, float radius)
    {
        float timeToWait = Time.timeSinceLevelLoad + timer;
        while (Time.timeSinceLevelLoad < timeToWait)
        {
            yield return null;
        }

        float terrZ = terrDat.size.z; //gets terrain Z
        float terrX = terrDat.size.x; //gets terrain X
        for (int i = 0; i < amount; i++)
        {
            GameObject go = Instantiate(list[Random.Range(0, list.Count)]); //sets gameobject to instantiation of game object
            Vector3 circle = (Random.insideUnitSphere * radius) + position; //gets random position within sphere and displaces it to the desired location
            circle.y = terr.SampleHeight(circle) + 1.0f; //adjusts y accordingly
            go.transform.position = circle; //sets position to new pos
        }
    }
}
