using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ComboSystem : MonoBehaviour
{

    private AICollision[] Zombies;
    [SerializeField] private float timerMax = 6.0f;
    private float timer = 0;
    private bool startTimer;
    private int zombiesKilledDuringComboTime = 0;
    TextMeshProUGUI ComboIndicator;
    Image SkullImage;

    private void Start()
    {
        ComboIndicator = GameObject.Find("ComboIndicator").GetComponent<TextMeshProUGUI>();
        SkullImage = GameObject.Find("SkullImage").GetComponent<Image>();
    }

    void Update()
    {
        Zombies = FindObjectsOfType<AICollision>();
        UpdateUI();
        CountDown();
    }

    void UpdateUI()
    {
        ComboIndicator.text = zombiesKilledDuringComboTime+"x";
    }

    void CountDown(){
        bool zombieIsDead=false;                                                        ///had to declare a local boolean since the 
                                                                                        ///timer -= Time.deltaTime;
        for (int i = 0; i < Zombies.Length; i++){                                       ///wouldn't work inside a for loop
            if (Zombies[i].isDead)
                zombieIsDead = true;
        }

        if (zombieIsDead){                                                              ///if a zombie dies
            timer = timerMax;                                                           ///reset the timer to 6
            zombiesKilledDuringComboTime++;                                             ///and increment the amount of zombies that have been killed
            SkullImage.DOColor(Color.white, 0.5f);                                      ///set the image and the 
            ComboIndicator.DOColor(Color.white, 0.5f);                                  ///indicator visible
        }

        else if (timer < 0.1f){                                                         ///when time's up
            timer = 0;                                                                  ///reset the timer
            zombiesKilledDuringComboTime = 0;                                           ///and the amount of zombies killed
            SkullImage.DOColor(Color.clear, 0f);                                      ///set the image
            ComboIndicator.DOColor(Color.clear, 0f);                                  ///and the indicator to transparent

        }
        
        timer -= Time.deltaTime;                                                    ///keep decrementing the value by 1 each second
    }

}