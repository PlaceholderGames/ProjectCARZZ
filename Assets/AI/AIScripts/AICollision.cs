
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
       if (collisionInfo.collider.tag == "Player" || collisionInfo.collider.tag == "Vehicle")
        {
            Destroy(ai, aiKillTimer);
            isDead = true;
            spawnObject = FindObjectOfType<SpawnObject>();
            spawnObject.aiNumber--;
        }
        //if (collisionInfo.collider.tag == "Vehicle")
        //{
        //    Destroy(ai, aiKillTimer);
        //    isDead = true;
        //}
    }

}
