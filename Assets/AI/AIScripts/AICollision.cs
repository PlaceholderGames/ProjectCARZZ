
using UnityEngine;

public class AICollision : MonoBehaviour
{
    
    private Animator anim;
    public float despawnTime = 5;
    
    public bool gaveDamage = false;
    public bool isDead = false;
    public bool hitPlayer = false;
    public bool ishit = false;
    private MSVehicleControllerFree vehicle;
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
            if (vehicle.KMh > 30)
            {
                isDead = true;
                Destroy(gameObject, despawnTime);
            }
            
            gaveDamage = true;
        }
        anim.SetBool("isWon", false);
    }

}
