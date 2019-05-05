using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ComboSystem : MonoBehaviour
{

    [SerializeField] private float timerMax = 6.0f;
    private float timer = 0;
    private int zombiesKilledDuringComboTime = 0;
    [SerializeField] private TextMeshProUGUI ComboIndicator;
    [SerializeField] private Image SkullImage;
    [SerializeField] private MSSceneControllerFree SceneController;
    private Camera playerCam;
    public Camera[] vehicleCameras;
    public MSVehicleControllerFree vehicle;
    public bool zombieIsDead = false;
    private ShakingCamera shaking;

    private void Awake()
    {
        if (!ComboIndicator)
            ComboIndicator = GameObject.Find("ComboIndicator").GetComponent<TextMeshProUGUI>();
        if (!SkullImage)
            SkullImage = GameObject.Find("SkullImage").GetComponent<Image>();
        if (!SceneController)
            SceneController = FindObjectOfType<MSSceneControllerFree>();
        
    }

    private void Start()
    {
        playerCam = GameObject.Find("Player").GetComponentInChildren<Camera>();
        vehicle = SceneController.vehicleCode;
    }

    void Update()
    {

       ComboIndicator.text = zombiesKilledDuringComboTime + "x";
        CountDown();
    }

    void CountDown(){
        ///had to declare a local boolean since the 
        /*
         * Can't loop through Zombies, we need the logic in the zombie script instead
         */

        ///timer -= Time.deltaTime;
        //for (int i = 0; i < Zombies.Length; i++){                                       ///wouldn't work inside a for loop
        //    if (Zombies[i].isDead)
        //        zombieIsDead = true;
        //}

        if (zombieIsDead){                                                              ///if a zombie dies
            timer = timerMax;                                                         ///reset the timer to 6
            //if (SceneController.vehicleCode.isInsideTheCar)
            //{
            //    shaking.TriggerShake(SceneController.vehicleCode.GetComponent<Camera>().transform);
            //}
            //else
            //{
            //    shaking.TriggerShake(playerCam.transform);
            //}
            
            zombiesKilledDuringComboTime++;                                             ///and increment the amount of zombies that have been killed
            SkullImage.DOColor(Color.white, 0f);                                      ///set the image and the 
            ComboIndicator.DOColor(Color.white, 0f);                                  ///indicator visible
            zombieIsDead = false;
        }

        else if (timer < 0.1f){                                                         ///when time's up
            SceneController.coinValue += BonusCalculator(zombiesKilledDuringComboTime); ///add coin based on the amount of zombies killed
            timer = 0;                                                                  ///reset the timer
            zombiesKilledDuringComboTime = 0;                                           ///reset the amount of zombies killed to 0
            SkullImage.DOColor(Color.clear, 0.5f);                                      ///set the image to transparent
            ComboIndicator.DOColor(Color.clear, 0.5f);                                  ///set the indicator to transparent

        }
        
        timer -= Time.deltaTime;                                                    ///keep decrementing the value by 1 each second
    }

    int BonusCalculator(int killCount)
    {
        int bonus = 0;
        bonus += (int)Mathf.Pow((killCount / 3), 2); 

        return bonus;
    }

}