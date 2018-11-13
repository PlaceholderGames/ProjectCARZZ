using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAI : MonoBehaviour {

    [HideInInspector]
    public DamageSystem ds;
    [HideInInspector]
    public LevelingSystem ls;
    private AICollision[] Ai;
    private AIBehaviour[] aIBehaviours;
    private MSFPSControllerFree player;
    private MSVehicleControllerFree vehicle;
    private Slider healthSlider;
    // Use this for initialization
    void Start () {
        ds = FindObjectOfType<DamageSystem>();
        ls = FindObjectOfType<LevelingSystem>();
        Ai = FindObjectsOfType<AICollision>();
        aIBehaviours = FindObjectsOfType<AIBehaviour>();
        player = FindObjectOfType<MSFPSControllerFree>();
        vehicle = FindObjectOfType<MSVehicleControllerFree>();
        healthSlider = GameObject.Find("healthSlider").GetComponent<Slider>();
        
	}
	
	// Update is called once per frame
	public void UpdateCheckAI() {
		for(int i = 0; i < Ai.Length; i++)
        {
            if (Ai[i].isDead)
            {
                if (vehicle.KMh > 30.0f)
                {
                    Debug.Log("GaveXP");
                    ls.currentXP += 1;
                    ls.UpdateLevelingSystem();
                    Ai[i].isDead = false;
                }
            }
            if (Ai[i].gaveDamage)
            {
                ds.RecievedDamage = Random.Range(1, 10); 
                healthSlider.value -= ds.RecievedDamage;
                Ai[i].gaveDamage = false;
            }
            if (player.gameObject.activeSelf)
            {
                aIBehaviours[i].target = player.transform;
            }
            else
            {

                aIBehaviours[i].target = vehicle.transform;
            }
        }
        
	}

    public float getCurrentXp()
    {
        return ls.currentXP;
    }

    public float getCurrentLevel()
    {
        return ls.currentLevel;
    }

    public float getTotalXp()
    {
        return ls.totalXP;
    }

    public float getRecievedDamage()
    {
        return ds.RecievedDamage;
    }
}
