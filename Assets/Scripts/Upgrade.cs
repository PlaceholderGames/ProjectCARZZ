using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class Upgrade : MonoBehaviour {

    public class Player {
        public int level, coin;

        public Player(int coins, int levels) {
            coin = coins;
            level = levels;
        }
    }
    public class Vehicle {
        public bool driftModeEnabled;
        public GameObject vehicleObj;
        public int maxSpeed, fuel, health, torque, unlockCostCoin, unlockCostLvl, isUnlocked;
        public Vehicle(GameObject vehicleObj, int isUnlocked, bool driftModeEnabled, int torque, int maxSpeed, int fuel, int health, int unlockCostCoin, int unlockCostLvl) {
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
    private int level, coin, fuel, repair, currentxp, totalxp;
    private GameObject upgradeButtons, lockIndicator, popUpPanel, unlockButton, buttonBackGround;
    private int currentVehicle;
    [SerializeField] private float rotationSpeed = 15.0f;

    private Text driftText, torque, speedText, fuelText, healthText, levelText, popUpText,unlockText;
    private TextMeshProUGUI tmp_lvltxt, tmp_cointxt;

    private void Initialize()
    {

        currentVehicle = 0;
        if (PlayerPrefs.GetInt("isNewGame") == 1)
        {
            coin = 0;
            level = 1;
            player = new Player(coin, level);

            PlayerPrefs.SetInt("v1_isUnlocked", 0);
            PlayerPrefs.SetInt("v2_isUnlocked", 1);
            PlayerPrefs.SetInt("v3_isUnlocked", 0);
            PlayerPrefs.SetInt("v2_torque", 25);
            PlayerPrefs.SetInt("v2_speed", 120);
            PlayerPrefs.SetInt("v2_fuel", 100);
            PlayerPrefs.SetInt("v2_health", 100);
            PlayerPrefs.SetInt("v3_torque", 40);
            PlayerPrefs.SetInt("v3_speed", 180);
            PlayerPrefs.SetInt("v3_fuel", 200);
            PlayerPrefs.SetInt("v3_health", 200);
            vehicle = new Vehicle[transform.childCount];
            vehicle[0] = new Vehicle(GameObject.FindGameObjectWithTag("Vehicle" + 1), 0, false, 12, 45, 0, 50, 1000000000, 20);
            vehicle[1] = new Vehicle(GameObject.FindGameObjectWithTag("Vehicle" + 2), 1, false, 25, 120, 100, 100, 250, 1);
            vehicle[2] = new Vehicle(GameObject.FindGameObjectWithTag("Vehicle" + 3), 0, false, 40, 180, 200, 200, 500, 10);
            
        }
        else if(PlayerPrefs.GetInt("isNewGame") == 0)
        {
            coin = PlayerPrefs.GetInt("coinValue");
            level = PlayerPrefs.GetInt("currentLevel");
            player = new Player(coin, level);

            vehicle = new Vehicle[transform.childCount];
            vehicle[0] = new Vehicle(GameObject.FindGameObjectWithTag("Vehicle" + 1), PlayerPrefs.GetInt("v1_isUnlocked"), false, 12, 45, 0, 50, 1000000000, 20);
            vehicle[1] = new Vehicle(GameObject.FindGameObjectWithTag("Vehicle" + 2), PlayerPrefs.GetInt("v2_isUnlocked"), false, PlayerPrefs.GetInt("v2_torque"), PlayerPrefs.GetInt("v2_speed"), PlayerPrefs.GetInt("v2_fuel"), PlayerPrefs.GetInt("v2_health"), 250, 1);
            vehicle[2] = new Vehicle(GameObject.FindGameObjectWithTag("Vehicle" + 3), PlayerPrefs.GetInt("v3_isUnlocked"), false, PlayerPrefs.GetInt("v3_torque"), PlayerPrefs.GetInt("v3_speed"), PlayerPrefs.GetInt("v3_fuel"), PlayerPrefs.GetInt("v3_health"), 500, 10);


        }


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
            vehicle[i].vehicleObj.transform.Rotate(0, rotationSpeed*Time.deltaTime, 0, Space.World);
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
            vehicle[currentVehicle].isUnlocked = 1;
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
        if (vehicle[currentVehicle].isUnlocked == 1) {
            StartCoroutine(upgradeButtonsAppear());
        }
        else if (vehicle[currentVehicle].isUnlocked == 0)
        {
            StartCoroutine(upgradeButtonsDisappear());
        }
    }
    public void selectVehicle()
    {
        SavePP();
        PlayerPrefs.SetInt("vehicleID", currentVehicle);
        if (PlayerPrefs.GetInt("isNewGame") == 1) PlayerPrefs.SetInt("isNewGame", 1);
        else if (PlayerPrefs.GetInt("isNewGame") == 0) PlayerPrefs.SetInt("isNewGame", 0);
        SceneManager.LoadScene("MapJoelV2");

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
        int minvalue = 0;
        if (isUpgrade == true && player.coin >= cost && upgradeable + value <= maxvalue)
        {
            upgradeable += value;
            player.coin -= cost;
            cost = Mathf.FloorToInt((cost + 10) * 0.6f);
        }
        else if (isUpgrade == false && (upgradeable - value) > minvalue)
        {
            upgradeable -= value;
            player.coin += cost;
        }

        else if ((upgradeable + value) > maxvalue)
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
        PlayerPrefs.SetInt("coinValue", player.coin);
        for (int i = 0; i < vehicle.Length; i++)
        {
            if (vehicle[i].vehicleObj.tag == "Vehicle1")
            {
                PlayerPrefs.SetInt("v1_torque", vehicle[0].torque);
                PlayerPrefs.SetInt("v1_speed", vehicle[0].maxSpeed);
                PlayerPrefs.SetInt("v1_fuel", vehicle[0].fuel);
                PlayerPrefs.SetInt("v1_health", vehicle[0].health);
                PlayerPrefs.SetInt("v1_isUnlocked", vehicle[0].isUnlocked);

            }
            else if (vehicle[i].vehicleObj.tag == "Vehicle2")
            {
                PlayerPrefs.SetInt("v2_torque", vehicle[1].torque);
                PlayerPrefs.SetInt("v2_speed", vehicle[1].maxSpeed);
                PlayerPrefs.SetInt("v2_fuel", vehicle[1].fuel);
                PlayerPrefs.SetInt("v2_health", vehicle[1].health);
                PlayerPrefs.SetInt("v2_isUnlocked", vehicle[1].isUnlocked);
            }

            else if (vehicle[i].vehicleObj.tag == "Vehicle3")
            {
                PlayerPrefs.SetInt("v3_torque", vehicle[2].torque);
                PlayerPrefs.SetInt("v3_speed", vehicle[2].maxSpeed);
                PlayerPrefs.SetInt("v3_fuel", vehicle[2].fuel);
                PlayerPrefs.SetInt("v3_health", vehicle[2].health);
                PlayerPrefs.SetInt("v3_isUnlocked", vehicle[2].isUnlocked);
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
                vehicle[0].isUnlocked = PlayerPrefs.GetInt("v1_isUnlocked");
            }
            else if (vehicle[i].vehicleObj.tag == "Vehicle2")
            {
                vehicle[1].torque = PlayerPrefs.GetInt("v2_torque");
                vehicle[1].maxSpeed = PlayerPrefs.GetInt("v2_speed");
                vehicle[1].fuel = PlayerPrefs.GetInt("v2_fuel");
                vehicle[1].health = PlayerPrefs.GetInt("v2_health");
                vehicle[1].isUnlocked = PlayerPrefs.GetInt("v2_isUnlocked");
            }

            else if (vehicle[i].vehicleObj.tag == "Vehicle3")
            {
                vehicle[2].torque = PlayerPrefs.GetInt("v3_torque");
                vehicle[2].maxSpeed = PlayerPrefs.GetInt("v3_speed");
                vehicle[2].fuel = PlayerPrefs.GetInt("v3_fuel");
                vehicle[2].health = PlayerPrefs.GetInt("v3_health");
                vehicle[2].isUnlocked = PlayerPrefs.GetInt("v3_isUnlocked");
            }
        }
    }
    
    private void Start(){
        Application.targetFrameRate = 60;
        Cursor.visible = true;
        Initialize();
        LoadPP();
    }
    private void Update(){
        rotateVehicle();
        lockVehicle();
        textUpdate();
    }
    
}
