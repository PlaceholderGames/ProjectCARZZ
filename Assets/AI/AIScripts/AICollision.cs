
using UnityEngine;

public class AICollision : MonoBehaviour
{
    
    private Animator anim;
    public float despawnTime = 5;
    
    public bool gavceDamage = false;
    public bool isDead = false;
    public bool hitPlayer = false;
    public bool ishit = false;
    private void Start()
    {
        
        anim = GetComponentInChildren<Animator>();
       
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
                Destroy(gameObject, despawnTime);
            else
            {
                anim.SetBool("isHit", true);
            }
        }
    }

}
