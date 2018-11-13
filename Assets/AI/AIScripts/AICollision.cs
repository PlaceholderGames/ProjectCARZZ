using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;

public class AICollision : MonoBehaviour
{
    private SpawnObject spawnObject;
    //private LevelingSystem damageSystem;
    //private Slider healthSlider;
    private int aiNumber;
    public GameObject ai;
    public bool isDead = false;
    public float aiKillTimer;
    //private Text popUpText;

    void Start()
    {
        //popUpText = GameObject.Find("popUpMsg").GetComponent<Text>();
        //damageSystem = FindObjectOfType<LevelingSystem>();
        //healthSlider = GameObject.Find("healthSlider").GetComponent<Slider>();
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
       if (collisionInfo.collider.tag == "Vehicle")//collisionInfo.collider.tag == "Player"
        {
            isDead = true;//sets variable to say its out of scene
            Destroy(ai, aiKillTimer);
            
            spawnObject = FindObjectOfType<SpawnObject>();//Finds the AI object
            spawnObject.CurrentNumberAi--;//Removes it from list of all ai on scene

            //Debug.Log(damageSystem.RecievedDamage);
            //healthSlider.value -= damageSystem.RecievedDamage;

        }

        if (collisionInfo.collider.tag == "Player")//collisionInfo.collider.tag == "Player"
        {
            collisionInfo.gameObject.SetActive(false);
            //popUpText.text = "You did Died Son";
        }
    }

}
