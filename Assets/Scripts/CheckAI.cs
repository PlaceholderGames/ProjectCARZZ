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
    private TreeCollision[] tc;
    private MSVehicleControllerFree vehicle;
    private Slider healthSlider;
    private Slider fs;
    private SpawnObject s;
    public bool displayFin;
    public bool displayDead;
    // Use this for initialization
    void Start () {
        ds = FindObjectOfType<DamageSystem>();
        ls = FindObjectOfType<LevelingSystem>();
        Ai = FindObjectsOfType<AICollision>();
        healthSlider = GameObject.Find("healthSlider").GetComponent<Slider>();
        fs = GameObject.Find("fuelSlider").GetComponent<Slider>();
        s = GameObject.Find("AISpawnArea").GetComponent<SpawnObject>();
        tc = FindObjectsOfType<TreeCollision>();

    }

    private void Update()
    {
        vehicle = FindObjectOfType<MSVehicleControllerFree>();
    }
    
    // Update is called once per frame
    public void UpdateCheckAI() {
        Ai = FindObjectsOfType<AICollision>();
        if(ls.finished())
        {
            displayFin = true;
        }
        for(int i = 0; i < tc.Length; i ++)
        {
            if(tc[i].gotHit)
            {
                ds.RecievedDamage = (10 + (int)Mathf.Sqrt(vehicle.KMh));
                healthSlider.value -= ds.RecievedDamage;
                tc[i].gotHit = false;
            }
        }
        for (int i = 0; i < Ai.Length; i++)
        {
                if(Ai[i].hitPlayer)
                {
                    displayDead = true;
                    break;
                }
                if (Ai[i].isDead)
                {

                    if (vehicle.KMh > 30.0f)
                    {
                        Debug.Log("GaveXP");
                        s.CurrentNumberAi--;
                        ls.currentXP += (10+(int)Mathf.Sqrt(vehicle.KMh));
                        ls.UpdateLevelingSystem();
                        Ai[i].isDead = false;
                    }
                }
                if (Ai[i].gaveDamage)
                {
                    ds.RecievedDamage = Random.Range(1, 10);
                    healthSlider.value -= ds.RecievedDamage;
                    fs.value += 2f;
                    Ai[i].gaveDamage = false;
                }
            
            Debug.Log("UpdateCheckAI Function");
            Debug.Log(Ai[i].isDead);
            
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
