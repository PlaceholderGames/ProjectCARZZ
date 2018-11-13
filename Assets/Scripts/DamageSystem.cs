using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Add script to MSPlayerControllerFree or MSVehicleControllerFree
public class DamageSystem : MonoBehaviour {

    public float MaxDamage;
    public float MinDamage;
    [HideInInspector]
    public float RecievedDamage; //Players/Vehicles health

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	public void UpdateDamageSystem() {
        
        for (int i = 0; i < FindObjectsOfType<AICollision>().Length; i++)
        {
            if (FindObjectsOfType<AICollision>()[i].isDead)
            {
                
                RecievedDamage = Random.Range(MinDamage, MaxDamage);

            }
            
        }

	}
}
