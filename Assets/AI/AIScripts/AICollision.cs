
using UnityEngine;

public class AICollision : MonoBehaviour
{
    public GameObject ai;
    private Animator anim;
    private void Start()
    {
        //anim = GetComponent<Animator>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
       if (collisionInfo.collider.tag == "Player")
        {
            anim.SetBool("isWon", true);
        }
        if (collisionInfo.collider.tag == "Vehicle")
        {
            Destroy(ai, 5);
        }
    }

}
