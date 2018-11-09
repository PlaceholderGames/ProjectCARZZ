
using UnityEngine;

public class AICollision : MonoBehaviour
{
    private SpawnObject spawnObject;
    private int aiNumber;
    public GameObject ai;
    public bool isDead = false;
    public int aiKillTimer;
    private void OnCollisionEnter(Collision collisionInfo)
    {
       if (collisionInfo.collider.tag == "Vehicle")//collisionInfo.collider.tag == "Player"
        {
            Destroy(ai, aiKillTimer);
            isDead = true;//sets variable to say its out of scene
            spawnObject = FindObjectOfType<SpawnObject>();//Finds the AI object
            spawnObject.CurrentNumberAi--;//Removes it from list of all ai on scene
        }
    }

}
