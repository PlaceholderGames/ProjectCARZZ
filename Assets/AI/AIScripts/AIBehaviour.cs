using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{

    public Rigidbody rb;
    public Transform target;
    public float moveSpeed = 5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce((target.position.x - transform.position.x) * moveSpeed * Time.deltaTime, 0, (target.position.z - transform.position.z) * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);

    }
}