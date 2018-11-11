
using UnityEngine;

public class AICollision : MonoBehaviour
{
    public GameObject ai;
    static Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
       if (collisionInfo.collider.tag == "target")
        {
            anim.SetBool("isWon", true);
        }
        if (collisionInfo.collider.tag == "Vehicle")
        {
            Destroy(ai, 5);
        }
    }

}
