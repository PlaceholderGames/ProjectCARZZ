using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem : MonoBehaviour
{

    public int currentLevel;
    private int maxLevel = 20;
    public int currentXP;
    public int totalXP;
    private AudioSource a;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("isNewGame") == 0)
        {
            currentLevel = PlayerPrefs.GetInt("currentLevel");
            currentXP = PlayerPrefs.GetInt("currentXP");
            totalXP = PlayerPrefs.GetInt("totalXP");
        }
        else if(PlayerPrefs.GetInt("isNewGame") == 1)
        {
            currentLevel = 1;
            currentXP = 0;
            totalXP = 10;
        }
        a = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void UpdateLevelingSystem()
    {
        if (currentXP >= totalXP)
        {
            if (currentXP > totalXP)
            {
                currentLevel++;
                currentXP = currentXP - totalXP;
                a.Play();
            }
            else
            {
                currentXP = 0;
                currentLevel++;
            }
            totalExperience(currentLevel);
        }
    }

    public bool finished()
    {
        if (currentLevel == maxLevel) return true;
        else return false;
    }

    private bool totalExperience(int lvl)
    {
        if (lvl == 1)
        {
            totalXP = totalXP * lvl;
            return true;
        }
        else if (lvl > 1)
        {
            totalXP = totalXP * lvl + (int)((float)totalXP / 2f);
            return true;
        }
        else if (lvl == maxLevel + 1)
        {
            currentXP = totalXP;
            return true;
        }
        else
        {
            return false;
        }
    }
}