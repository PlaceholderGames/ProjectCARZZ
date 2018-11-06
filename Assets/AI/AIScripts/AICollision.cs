
using UnityEngine;

public class AICollision : MonoBehaviour
{
    public GameObject ai;
    private void OnCollisionEnter(Collision collisionInfo)
    {
       if (collisionInfo.collider.tag == "target")
        { }
        if (collisionInfo.collider.tag == "Vehicle")
        {
            Destroy(ai, 5);
        }
    }

}
