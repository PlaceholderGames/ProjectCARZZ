using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Add script to Player or Vehicle
public class DamageSystem : MonoBehaviour {

    private AICollision[] Ai;
    public float MaxDamage;
    public float MinDamage;
    [HideInInspector]
    public float RecievedDamage; //Players/Vehicles health

    // Use this for initialization
    void Start () {
        Ai = FindObjectOfType<AICollision>();
	}
	
	// Update is called once per frame
	void Update () {
        Ai = FindObjectsOfType<AICollision>();
        for (int i = 0; i < Ai.Length; i++)
        {
            if (Ai[i].isDead)
            {
                RecievedDamage = Random.Range(MinDamage, MaxDamage);
            }
        }

	}
}
