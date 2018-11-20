using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Upgrade : MonoBehaviour {

    public class Player{
        public int coin, xp;
        public Player(int coins, int xps){
            coin = coins;
            xp = xps;
        }
    }

    public class Vehicle{
        public bool isUnlocked, driftModeEnabled;
        public GameObject vehicleObj;
        public int maxSpeed, fuel, health;
        public float tourqe;

        public Vehicle(GameObject vehicleObj, bool isUnlocked, bool driftModeEnabled, float tourqe, int maxSpeed, int fuel, int health){
            this.vehicleObj = vehicleObj;
            this.isUnlocked = isUnlocked;
            this.driftModeEnabled = driftModeEnabled;
            this.tourqe = tourqe;
            this.maxSpeed = maxSpeed;
            this.fuel = fuel;
            this.health = health;
        }
    }

    public Vehicle[] vehicle;
    public Player player;
    private int xp, coin;

    public GameObject upgradeButtons;
    public GameObject image;
    public Button button;
    public int currentVehicle;

    private Text driftText, tourqeText, speedText, fuelText, healthText;

    private void Initalize()
    {
        coin = PlayerPrefs.GetInt("coinValue");
        xp = PlayerPrefs.GetInt("currentXp");
        player = new Player(coin, xp);

        vehicle = new Vehicle[transform.childCount];
        vehicle[0] = new Vehicle(GameObject.FindGameObjectWithTag("Vehicle1"), true, false, PlayerPrefs.GetFloat("v1_torque"), PlayerPrefs.GetInt("v1_speed"), PlayerPrefs.GetInt("v1_fuel"), PlayerPrefs.GetInt("v1_health"));
        vehicle[1] = new Vehicle(GameObject.FindGameObjectWithTag("Vehicle2"), false, false, PlayerPrefs.GetFloat("v2_torque"), PlayerPrefs.GetInt("v2_speed"), PlayerPrefs.GetInt("v2_fuel"), PlayerPrefs.GetInt("v2_health"));
        vehicle[2] = new Vehicle(GameObject.FindGameObjectWithTag("Vehicle3"), false, false, PlayerPrefs.GetFloat("v3_torque"), PlayerPrefs.GetInt("v3_speed"), PlayerPrefs.GetInt("v3_fuel"), PlayerPrefs.GetInt("v3_health"));

        

        foreach (Vehicle vh in vehicle)
            vh.vehicleObj.SetActive(false);
        if (vehicle[0].vehicleObj)
            vehicle[0].vehicleObj.SetActive(true);

        driftText = GameObject.Find("driftText").GetComponent<Text>();
        tourqeText = GameObject.Find("tourqeText").GetComponent<Text>();
        speedText = GameObject.Find("speedText").GetComponent<Text>();
        fuelText = GameObject.Find("fuelText").GetComponent<Text>();
        healthText = GameObject.Find("healthText").GetComponent<Text>();
    }
    private void rotateVehicle()
    {
            vehicle[currentVehicle].vehicleObj.transform.Rotate(0, 0.5f, 0, Space.World);
    }


    public void nextVehicle(){
        vehicle[currentVehicle].vehicleObj.SetActive(false);
        currentVehicle++;
        if (currentVehicle == vehicle.Length)
            currentVehicle = 0;
        vehicle[currentVehicle].vehicleObj.SetActive(true);
    }
    public void previousVehicle(){
        vehicle[currentVehicle].vehicleObj.SetActive(false);
        currentVehicle--;
        if (currentVehicle < 0)
            currentVehicle = vehicle.Length - 1;

        vehicle[currentVehicle].vehicleObj.SetActive(true);
    }
    public void unlockVehicle(){
        image.SetActive(false);
        button.gameObject.SetActive(false);
        vehicle[currentVehicle].isUnlocked = true;
    }
    public void lockVehicle()
    {
        if(vehicle[currentVehicle].isUnlocked==true){
            image.SetActive(false);
            button.gameObject.SetActive(false);
            upgradeButtons.SetActive(true);
        }
        else{
            image.SetActive(true);
            button.gameObject.SetActive(true);
            upgradeButtons.SetActive(false);
        }
    }

    public void selectVehicle()
    {
        PlayerPrefs.SetString("vehicleChoice",vehicle[currentVehicle].vehicleObj.tag);
        SceneManager.LoadScene(1);

    }
    public void upgradeSpeed(bool upgrade)
    {
        if (upgrade == true)
            vehicle[currentVehicle].maxSpeed += 10;
        else
            vehicle[currentVehicle].maxSpeed -= 10;
    }
    public void upgradeTourqe(bool upgrade)
    {
        if (upgrade == true)
            vehicle[currentVehicle].tourqe += 1;
        else
            vehicle[currentVehicle].tourqe -= 1;
    }
    public void upgradeFuel(bool upgrade)
    {
        if (upgrade == true)
            vehicle[currentVehicle].fuel += 10;
        else
            vehicle[currentVehicle].fuel -= 10;
    }
    public void upgradeHealth(bool upgrade)
    {
        if (upgrade == true)
            vehicle[currentVehicle].health += 10;
        else
            vehicle[currentVehicle].health -= 10;
    }
    public void enableDrift(bool upgrade)
    {
        if (upgrade == true)
            vehicle[currentVehicle].driftModeEnabled = true;
        else
            vehicle[currentVehicle].driftModeEnabled = false;
    }

    void SavePP()
    {
        for (int i = 0; i < vehicle.Length; i++)
        {
            if (vehicle[i].vehicleObj.tag == "Vehicle1")
            {
                PlayerPrefs.SetFloat("v1_torque", vehicle[0].tourqe);
                PlayerPrefs.SetInt("v1_speed", vehicle[0].maxSpeed);
                PlayerPrefs.SetInt("v1_fuel", vehicle[0].fuel);
                PlayerPrefs.SetInt("v1_health", vehicle[0].health);
            }
            else if (vehicle[i].vehicleObj.tag == "Vehicle2")
            {
                PlayerPrefs.SetFloat("v2_torque", vehicle[1].tourqe);
                PlayerPrefs.SetInt("v2_speed", vehicle[1].maxSpeed);
                PlayerPrefs.SetInt("v2_fuel", vehicle[1].fuel);
                PlayerPrefs.SetInt("v2_health", vehicle[1].health);

            }

            else if (vehicle[i].vehicleObj.tag == "Vehicle3")
            {
                PlayerPrefs.SetFloat("v3_torque", vehicle[2].tourqe);
                PlayerPrefs.SetInt("v3_speed", vehicle[2].maxSpeed);
                PlayerPrefs.SetInt("v3_fuel", vehicle[2].fuel);
                PlayerPrefs.SetInt("v3_health", vehicle[2].health);

            }
        }
    }

    void LoadPP()
    {
        for (int i = 0; i < vehicle.Length; i++)
        {
            if (vehicle[i].vehicleObj.tag == "Vehicle1")
            {
                vehicle[0].tourqe = PlayerPrefs.GetFloat("v1_torque");
                vehicle[0].maxSpeed = PlayerPrefs.GetInt("v1_speed");
                vehicle[0].fuel = PlayerPrefs.GetInt("v1_fuel");
                vehicle[0].health = PlayerPrefs.GetInt("v1_health");
            }
            else if (vehicle[i].vehicleObj.tag == "Vehicle2")
            {
                vehicle[1].tourqe = PlayerPrefs.GetFloat("v2_torque");
                vehicle[1].maxSpeed= PlayerPrefs.GetInt("v2_speed");
                vehicle[1].fuel = PlayerPrefs.GetInt("v2_fuel");
                vehicle[1].health = PlayerPrefs.GetInt("v2_health");

            }

            else if (vehicle[i].vehicleObj.tag == "Vehicle3")
            {
                vehicle[2].tourqe = PlayerPrefs.GetFloat("v3_torque");
                vehicle[2].maxSpeed = PlayerPrefs.GetInt("v3_speed");
                vehicle[2].fuel = PlayerPrefs.GetInt("v3_fuel");
                vehicle[2].health = PlayerPrefs.GetInt("v3_health");

            }
        }
    }

    private void Start(){
        Initalize();
        LoadPP();
    }

    private void Update(){
        rotateVehicle();
        lockVehicle();
        driftText.text = ""+vehicle[currentVehicle].driftModeEnabled;
        tourqeText.text = "" + vehicle[currentVehicle].tourqe;
        speedText.text = "" + vehicle[currentVehicle].maxSpeed;
        fuelText.text = "" + vehicle[currentVehicle].fuel;
        healthText.text = "" + vehicle[currentVehicle].health;
        SavePP();
    }
    
}
