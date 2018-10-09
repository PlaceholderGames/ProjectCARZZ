
using UnityEngine;

public class AICollision : MonoBehaviour
{
    public GameObject ai;
    private void OnCollisionEnter(Collision collisionInfo)
    {
        Debug.Log(collisionInfo.collider.tag);
        if (collisionInfo.collider.tag == "target")
            Destroy(ai, 5);
    }

}
