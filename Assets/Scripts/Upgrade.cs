using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class Upgrade : MonoBehaviour {

    public class Player {
        public int coin, level;
        public Player(int coins, int levels) {
            coin = coins;
            level = levels;
        }
    }
    public class Vehicle {
        public bool isUnlocked, driftModeEnabled;
        public GameObject vehicleObj;
        public int maxSpeed, fuel, health, torque, unlockCostCoin, unlockCostLvl;
        public Vehicle(GameObject vehicleObj, bool isUnlocked, bool driftModeEnabled, int torque, int maxSpeed, int fuel, int health, int unlockCostCoin, int unlockCostLvl) {
            this.vehicleObj = vehicleObj;
            this.isUnlocked = isUnlocked;
            this.driftModeEnabled = driftModeEnabled;
            this.torque = torque;
            this.maxSpeed = maxSpeed;
            this.fuel = fuel;
            this.health = health;
            this.unlockCostLvl = unlockCostLvl;
            this.unlockCostCoin = unlockCostCoin;
        }

    }

    private Vehicle[] vehicle;
    private Player player;
    private int level, coin;
    private GameObject upgradeButtons, lockIndicator, popUpPanel, unlockButton, buttonBackGround;
    private int currentVehicle;

    private Text driftText, torque, speedText, fuelText, healthText, levelText, popUpText,unlockText;
    private TextMeshProUGUI tmp_lvltxt, tmp_cointxt;

    private void Initialize()
    {
        //   coin = PlayerPrefs.GetInt("coinValue");
        //   xp = PlayerPrefs.GetInt("currentXp");
        coin = 999;
        level = 50;
        player = new Player(coin, level);

        vehicle = new Vehicle[transform.childCount];
        vehicle[0] = new Vehicle(GameObject.FindGameObjectWithTag("Vehicle1"), true, false, 12, 45, 0, 50, 0, 0);
        vehicle[1] = new Vehicle(GameObject.FindGameObjectWithTag("Vehicle2"), false, false,25, 120, 100, 100,250, 5);
        vehicle[2] = new Vehicle(GameObject.FindGameObjectWithTag("Vehicle3"), false, false, 40, 180, 200, 200,500,15);



        foreach (Vehicle vh in vehicle)
            vh.vehicleObj.SetActive(false);
        if (vehicle[0].vehicleObj)
            vehicle[0].vehicleObj.SetActive(true);

        popUpPanel = GameObject.Find("popUpPanel");
        unlockButton = GameObject.Find("unlockButton");
        lockIndicator = GameObject.Find("lockIndicator");
        upgradeButtons = GameObject.Find("UpgradeButtons");
        driftText = GameObject.Find("driftText").GetComponent<Text>();
        torque = GameObject.Find("torqueText").GetComponent<Text>();
        speedText = GameObject.Find("speedText").GetComponent<Text>();
        fuelText = GameObject.Find("fuelText").GetComponent<Text>();
        healthText = GameObject.Find("healthText").GetComponent<Text>();
        tmp_cointxt = GameObject.Find("tmp_cointxt").GetComponent<TextMeshProUGUI>();
        popUpPanel = GameObject.Find("PopUpPanel");
        popUpText = GameObject.Find("PopUpPanel").GetComponentInChildren<Text>();
        popUpPanel.SetActive(false);
        tmp_lvltxt = GameObject.Find("tmp_lvltxt").GetComponent<TextMeshProUGUI>();
        unlockText = GameObject.Find("unlockText").GetComponent<Text>();
        buttonBackGround = GameObject.Find("buttonBackGround");
    } 
    private void rotateVehicle(){
        for(int i=0; i<vehicle.Length;i++)
            vehicle[i].vehicleObj.transform.Rotate(0, 0.5f, 0, Space.World);
    } 
    private void textUpdate()
    {
        driftText.text = "Drift mode:" + vehicle[currentVehicle].driftModeEnabled;
        torque.text = "Torque:" + vehicle[currentVehicle].torque;
        speedText.text = "Max speed:" + vehicle[currentVehicle].maxSpeed;
        fuelText.text = "Fuel capacity:" + vehicle[currentVehicle].fuel;
        healthText.text = "Max health:" + vehicle[currentVehicle].health;
        tmp_cointxt.text = "" + player.coin;
        tmp_lvltxt.text = "LvL: " + player.level;
        unlockText.text = "Unlock for " + vehicle[currentVehicle].unlockCostCoin;
    } 

    public void nextVehicle() {
        vehicle[currentVehicle].vehicleObj.SetActive(false);
        currentVehicle++;
        if (currentVehicle == vehicle.Length)
            currentVehicle = 0;
        vehicle[currentVehicle].vehicleObj.SetActive(true);

    }
    public void previousVehicle() {
        vehicle[currentVehicle].vehicleObj.SetActive(false);
        currentVehicle--;
        if (currentVehicle < 0)
            currentVehicle = vehicle.Length - 1;

        vehicle[currentVehicle].vehicleObj.SetActive(true);
    }
    public void unlockVehicle() {
        if (vehicle[currentVehicle].unlockCostLvl <= player.level && vehicle[currentVehicle].unlockCostCoin <= player.coin){
            lockIndicator.SetActive(false);
            unlockButton.SetActive(false);
            vehicle[currentVehicle].isUnlocked = true;
            player.coin -= vehicle[currentVehicle].unlockCostCoin;
            StartCoroutine(popUpMsgPop("Vehicle unlocked!"));
        }
        else if (vehicle[currentVehicle].unlockCostLvl > player.level)
            StartCoroutine(popUpMsgPop("Low LVL"));
        else if (vehicle[currentVehicle].unlockCostCoin > player.coin)
            StartCoroutine(popUpMsgPop("Low COIN"));
        else
            StartCoroutine(popUpMsgPop("IDK boii"));
    }
    public void lockVehicle()
    {
        if (vehicle[currentVehicle].isUnlocked == true) {
            StartCoroutine(upgradeButtonsAppear());
        }
        else {
            StartCoroutine(upgradeButtonsDisappear());
        }
    }
    public void selectVehicle()
    {
        PlayerPrefs.SetString("vehicleChoice", vehicle[currentVehicle].vehicleObj.tag);
        SceneManager.LoadScene(1);

    }

    IEnumerator popUpMsgPop(string msg)
    {
        popUpPanel.SetActive(true);
        popUpText.text = msg;
        popUpPanel.GetComponent<SpriteRenderer>().DOColor(Color.white, 1f);
        popUpText.DOColor(Color.black, 1f);
        yield return new WaitForSeconds(2f);
        popUpPanel.GetComponent<SpriteRenderer>().DOColor(Color.clear, 1f);
        popUpText.DOColor(Color.clear, 1);
        yield return new WaitForSeconds(1);
        popUpPanel.SetActive(false);
    }
    IEnumerator upgradeButtonsAppear()
    {
        upgradeButtons.SetActive(true);
        lockIndicator.SetActive(false);
        unlockButton.SetActive(false);
        buttonBackGround.transform.DOScaleX(4.85f, 1);

        yield return new WaitForSeconds(0);
    }
    IEnumerator upgradeButtonsDisappear()
    {
        upgradeButtons.SetActive(false);
        lockIndicator.SetActive(true);
        unlockButton.SetActive(true);
        yield return new WaitForSeconds(0);
        buttonBackGround.transform.DOScaleX(1.5f, 1);
    }

    private void upgradeSomething(bool isUpgrade, ref int upgradeable, ref int cost, int value, int maxvalue)
    {
        if (isUpgrade == true && player.coin >= cost && upgradeable + value <= maxvalue)
        {
            upgradeable += value;
            player.coin -= cost;
            cost = Mathf.FloorToInt((cost + 10) * 0.6f);
            SavePP();
        }
        else if (isUpgrade == false)
            upgradeable -= value;
        else if (upgradeable + value > maxvalue)
        {
            //gameObject.GetComponent<Button>().interactable = false;
            StartCoroutine(popUpMsgPop("max level reached!"));
        }

        else if (cost > player.coin)
            StartCoroutine(popUpMsgPop("Get more coin boi"));
        else
            Debug.Log("U fckd up something");
    }
    public void upgradeSpeed(bool isUpgrade){
        int cost = 10;
        upgradeSomething(isUpgrade,ref vehicle[currentVehicle].maxSpeed, ref cost, 10, 280);
    }
    public void upgradeTourqe(bool isUpgrade){
        int cost = 15;
        upgradeSomething(isUpgrade, ref vehicle[currentVehicle].torque, ref cost, 1, 50);
    }
    public void upgradeFuel(bool isUpgrade){
        int cost = 20;
        if (vehicle[currentVehicle].vehicleObj.tag != "Vehicle1")
            upgradeSomething(isUpgrade, ref vehicle[currentVehicle].fuel, ref cost, 10, 200);
        else
        {
            cost = 0;
            upgradeSomething(isUpgrade, ref vehicle[currentVehicle].fuel, ref cost, 0, 0);
        }

    }
    public void upgradeHealth(bool isUpgrade){
        int cost = 20;
        upgradeSomething(isUpgrade, ref vehicle[currentVehicle].health, ref cost, 10, 200);
    }
    public void enableDrift(bool isUpgrade)
    {
        if (isUpgrade == true)
            vehicle[currentVehicle].driftModeEnabled = true;
        else if (isUpgrade == false)
            vehicle[currentVehicle].driftModeEnabled = false;
    }

    private void SavePP()
    {
        for (int i = 0; i < vehicle.Length; i++)
        {
            if (vehicle[i].vehicleObj.tag == "Vehicle1")
            {
                PlayerPrefs.SetFloat("v1_torque", vehicle[0].torque);
                PlayerPrefs.SetInt("v1_speed", vehicle[0].maxSpeed);
                PlayerPrefs.SetInt("v1_fuel", vehicle[0].fuel);
                PlayerPrefs.SetInt("v1_health", vehicle[0].health);
            }
            else if (vehicle[i].vehicleObj.tag == "Vehicle2")
            {
                PlayerPrefs.SetFloat("v2_torque", vehicle[1].torque);
                PlayerPrefs.SetInt("v2_speed", vehicle[1].maxSpeed);
                PlayerPrefs.SetInt("v2_fuel", vehicle[1].fuel);
                PlayerPrefs.SetInt("v2_health", vehicle[1].health);

            }

            else if (vehicle[i].vehicleObj.tag == "Vehicle3")
            {
                PlayerPrefs.SetFloat("v3_torque", vehicle[2].torque);
                PlayerPrefs.SetInt("v3_speed", vehicle[2].maxSpeed);
                PlayerPrefs.SetInt("v3_fuel", vehicle[2].fuel);
                PlayerPrefs.SetInt("v3_health", vehicle[2].health);

            }
        }
    }
    private void LoadPP()
    {
        for (int i = 0; i < vehicle.Length; i++)
        {
            if (vehicle[i].vehicleObj.tag == "Vehicle1")
            {
                vehicle[0].torque = PlayerPrefs.GetInt("v1_torque");
                vehicle[0].maxSpeed = PlayerPrefs.GetInt("v1_speed");
                vehicle[0].fuel = PlayerPrefs.GetInt("v1_fuel");
                vehicle[0].health = PlayerPrefs.GetInt("v1_health");
            }
            else if (vehicle[i].vehicleObj.tag == "Vehicle2")
            {
                vehicle[1].torque = PlayerPrefs.GetInt("v2_torque");
                vehicle[1].maxSpeed= PlayerPrefs.GetInt("v2_speed");
                vehicle[1].fuel = PlayerPrefs.GetInt("v2_fuel");
                vehicle[1].health = PlayerPrefs.GetInt("v2_health");

            }

            else if (vehicle[i].vehicleObj.tag == "Vehicle3")
            {
                vehicle[2].torque = PlayerPrefs.GetInt("v3_torque");
                vehicle[2].maxSpeed = PlayerPrefs.GetInt("v3_speed");
                vehicle[2].fuel = PlayerPrefs.GetInt("v3_fuel");
                vehicle[2].health = PlayerPrefs.GetInt("v3_health");

            }
        }
    }
    
    private void Start(){

        Initialize();
        //LoadPP();
    }
    private void Update(){
        rotateVehicle();
        lockVehicle();
        textUpdate();
    }
    
}
