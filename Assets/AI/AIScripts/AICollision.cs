
using UnityEngine;

public class AICollision : MonoBehaviour
{
    private Animator anim;
    private SpawnObject spawnObject;
    private MSVehicleControllerFree[] vehicles;


    public float despawnTime = 0.01f;
    public bool gaveDamage = false;
    public bool isDead = false;
    public bool hitPlayer = false;
    public bool ishit = false;
    public float killaispeed = 30.0f;
  
    private void Start()
    {
        
        anim = GetComponentInChildren<Animator>();
        vehicles = FindObjectsOfType<MSVehicleControllerFree>();
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
       if (collisionInfo.collider.tag == "Player")
        {
            hitPlayer = true;
            anim.SetBool("isWon", true);
        }


       if (collisionInfo.collider.tag == "Vehicle" || collisionInfo.collider.tag == "Vehicle1" || collisionInfo.collider.tag == "Vehicle2" || collisionInfo.collider.tag == "Vehicle3" || collisionInfo.collider.tag == "activeVehicle")
        {
            for(int i = 0; i<vehicles.Length;i++)
            {
                if (vehicles[i].KMh > killaispeed)
                {
                    isDead = true;
                    Destroy(gameObject, despawnTime);
                    spawnObject = FindObjectOfType<SpawnObject>();//Finds the AI object
                    spawnObject.CurrentNumberAi--;//Removes it from list of all ai on scene
                }
                gaveDamage = true;
            }

        }
    }

}
