
using UnityEngine;

public class AICollision : MonoBehaviour
{
    private Animator anim;
    private SpawnObject spawnObject;
    private MSVehicleControllerFree vehicle;


    public float despawnTime = 5;
    public bool gaveDamage = false;
    public bool isDead = false;
    public bool hitPlayer = false;
    public bool ishit = false;
  
    private void Start()
    {
        
        anim = GetComponentInChildren<Animator>();
        vehicle = FindObjectOfType<MSVehicleControllerFree>();
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
       if (collisionInfo.collider.tag == "Player")
        {
            hitPlayer = true;
            anim.SetBool("isWon", true);
        }
        if (collisionInfo.collider.tag == "Vehicle")
        {
            if (vehicle.KMh > 30.0f)
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
