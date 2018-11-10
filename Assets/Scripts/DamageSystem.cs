using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Add script to MSPlayerControllerFree or MSVehicleControllerFree
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
	void UpdateDamageSystem() { // called in MSSceneController Update function
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
