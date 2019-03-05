using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;
using DG.Tweening;

[Serializable]
public class ControlsFree {
	[Space(10)][Tooltip("If this variable is true, the control for this variable will be activated.")]
	public bool enable_reloadScene_Input = true;
	[Tooltip("The key that must be pressed to reload the current scene.")]
	public KeyCode reloadScene = KeyCode.R;

	[Space(10)][Tooltip("If this variable is true, the control for this variable will be activated.")]
	public bool enable_startTheVehicle_Input = true;
	[Tooltip("The key that must be pressed to turn the vehicle engine on or off.")]
	public KeyCode startTheVehicle = KeyCode.F;

	[Space(10)][Tooltip("If this variable is true, the control for this variable will be activated.")]
	public bool enable_enterEndExit_Input = true;
	[Tooltip("The key that must be pressed to enter or exit the vehicle.")]
	public KeyCode enterEndExit = KeyCode.T;

	[Space(10)][Tooltip("If this variable is true, the control for this variable will be activated.")]
	public bool enable_handBrakeInput_Input = true;
	[Tooltip("The key that must be pressed to activate or deactivate the vehicle hand brake.")]
	public KeyCode handBrakeInput = KeyCode.Space;


    [Space(10)][Tooltip("If this variable is true, the control for this variable will be activated.")]
	public bool enable_switchingCameras_Input = true;
	[Tooltip("The key that must be pressed to toggle between the cameras of the vehicle.")]
	public KeyCode switchingCameras = KeyCode.C;

	[Space(10)][Tooltip("If this variable is true, the control for this variable will be activated.")]
	public bool enable_pause_Input = true;
	[Tooltip("The key that must be pressed to pause the game.")]
	public KeyCode pause = KeyCode.Escape;
}

public class MSSceneControllerFree : MonoBehaviour {

    #region defineInputs
    [Tooltip("Vertical input recognized by the system")]
    public string _verticalInput = "Vertical";

    [Tooltip("Horizontal input recognized by the system")]
    public string _horizontalInput = "Horizontal";

    [Tooltip("Horizontal input for camera movements")]
    public string _mouseXInput = "Mouse X";

    [Tooltip("Vertical input for camera movements")]
    public string _mouseYInput = "Mouse Y";

    [Tooltip("Scroll input, to zoom in and out of the cameras.")]
    public string _mouseScrollWheelInput = "Mouse ScrollWheel";
    #endregion

    public enum ControlTypeFree { windows, mobileButton };

    [Space(10)] [Tooltip("Here you can configure the vehicle controls, choose the desired inputs and also, deactivate the unwanted ones.")]
    public ControlsFree controls;
    [Tooltip("All vehicles in the scene containing the 'MS Vehicle Controller' component must be associated with this list.")]
    public GameObject[] vehicles;
    [Space(10)] [Tooltip("This variable is responsible for defining the vehicle in which the player will start. It represents an index of the 'vehicles' list, where the number placed here represents the index of the list. The selected index will be the starting vehicle.")]
    public int startingVehicle = 0;
    [Tooltip("The player must be associated with this variable. This variable should only be used if your scene also has a player other than a vehicle. This \"player\" will temporarily be disabled when you get in a vehicle, and will be activated again when you leave a vehicle.")]
    public GameObject player;
    [Tooltip("If this variable is true and if you have a player associated with the 'player' variable, the game will start at the player. Otherwise, the game will start in the starting vehicle, selected in the variable 'startingVehicle'.")]
    public bool startInPlayer = false;
    [Tooltip("Here you can select the type of control, where 'Mobile Button' will cause buttons to appear on the screen so that vehicles can be controlled, 'Mobile Joystick' will cause two Joysticks to appear on the screen so vehicles can be controlled, And 'windows' will allow vehicles to be controlled through the computer.")]
    public ControlTypeFree selectControls = ControlTypeFree.windows;
    [Tooltip("This is the minimum distance the player needs to be in relation to the door of a vehicle, to interact with it.")]
    public float minDistance = 3;
    [Space(10)] [Tooltip("If this variable is true, useful data will appear on the screen, such as the car's current gear, speed, brakes, among other things.")]
    public bool UIVisualizer = true;

    JoystickFree joystickCamera;
    Button cameraMobileButton;
    Button enterAndExitButton;
    MSButtonFree buttonLeft;
    MSButtonFree buttonRight;
    MSButtonFree buttonUp;
    MSButtonFree buttonDown;
    //
    Button nextVehicle;
    Button previousVehicle;

    Text gearText;
    Text mphText;

    #region customizeInputs
    [HideInInspector]
    public float verticalInput = 0;
    [HideInInspector]
    public float horizontalInput = 0;
    [HideInInspector]
    public float mouseXInput = 0;
    [HideInInspector]
    public float mouseYInput = 0;
    [HideInInspector]
    public float mouseScrollWheelInput = 0;
    #endregion

    [SerializeField] private int fuelDecreaseValue = 100000;

    int currentVehicle = 0;
    int clampGear;
    int proximityObjectIndex;
    int proximityDoorIndex;
    bool blockedInteraction = false;
    bool pause = false;
    bool error;
    bool enterAndExitBool;
    string sceneName;

    public MSVehicleControllerFree vehicleCode;
    MSVehicleControllerFree controllerTemp;
    float currentDistanceTemp;
    float proximityDistanceTemp;

    float MSbuttonHorizontal;
    float MSbuttonVertical;

    bool playerIsNull;

    Vector2 vectorDirJoystick;



    //ADDONS BY RICHARD
    private Slider levelSlider;
    private Text LevelText;
    private Text XpText;

    private Text kmhText;
    private Text gearTxt;

    public Slider fuelSlider;
    private Text fuelText;
    public int fuelValue;

    private Text coinText;
    public int coinValue;

    private LevelingSystem ls;

    private Slider healthSlider;
    private Text repairText;
    public int repairValue;

    public GameObject popUpMsg;
    private GameObject speedUI;

    private int vehicleID;
    private OpenVehicleMenu ovm;
    private AICollision[] ai;
    bool wasupfor5sec;
    bool wasupfor5secagain;
    float level;


    // Dewy's addons
    public bool Pause = false;
    public GameObject pauseMenuPanel;
    public bool insidePetrolStation;

    void Start()
    {
        kmhText = GameObject.Find("kmhTxt").GetComponent<Text>();
        gearTxt = GameObject.Find("gearTxt").GetComponent<Text>();
        healthSlider = GameObject.Find("healthSlider").GetComponent<Slider>();
        fuelSlider = GameObject.Find("fuelSlider").GetComponent<Slider>();
        fuelText = GameObject.Find("fuelText").GetComponent<Text>();
        repairText = GameObject.Find("repairText").GetComponent<Text>();
        coinText = GameObject.Find("coinText").GetComponent<Text>();
        levelSlider = GameObject.Find("levelSlider").GetComponent<Slider>();
        XpText = GameObject.Find("XpText").GetComponent<Text>();
        LevelText = GameObject.Find("levelText").GetComponent<Text>();
        speedUI = GameObject.Find("speedUI");
        ls = FindObjectOfType<LevelingSystem>();
        ovm = FindObjectOfType<OpenVehicleMenu>();

        vehicleID = PlayerPrefs.GetInt("vehicleID")-1;
        InitVehicle();
        LoadPP();

        if (PlayerPrefs.GetInt("isNewGame") == 1)
        {
            fuelValue = 0;
            repairValue = 0;
            coinValue = PlayerPrefs.GetInt("coinValue"); ;
            PlayerPrefs.SetInt("fuelValue", 0);
            PlayerPrefs.SetInt("repairValue", 0);
            PlayerPrefs.SetInt("v2_health", (int)healthSlider.maxValue);
            PlayerPrefs.SetInt("v2_currenthealth", (int)healthSlider.maxValue);
            PlayerPrefs.SetInt("v3_currenthealth", PlayerPrefs.GetInt("v3_health"));
            PlayerPrefs.SetInt("v2_fuel", (int)fuelSlider.maxValue);
            PlayerPrefs.SetInt("v3_currentfuel", PlayerPrefs.GetInt("v3_fuel"));
            PlayerPrefs.SetInt("v2_currentfuel", (int)fuelSlider.maxValue);
        }
        else if (PlayerPrefs.GetInt("isNewGame") == 0)
        {
            fuelValue = PlayerPrefs.GetInt("fuelValue");
            repairValue = PlayerPrefs.GetInt("repairValue");
            coinValue = PlayerPrefs.GetInt("coinValue");

            if (vehicles[0].activeSelf)
            {
                healthSlider.value = PlayerPrefs.GetInt("v2_currenthealth");
                fuelSlider.value = PlayerPrefs.GetInt("v2_currentfuel");
            }
            else if (vehicles[1].activeSelf)
            {
                healthSlider.value = PlayerPrefs.GetInt("v3_currenthealth");
                fuelSlider.value = PlayerPrefs.GetInt("v3_currentfuel");
            }
        }



    }

    public void SavePP()
    {
        PlayerPrefs.SetInt("coinValue", coinValue);
        PlayerPrefs.SetInt("currentLevel", (int)level);
        PlayerPrefs.SetInt("fuelValue", fuelValue);
        PlayerPrefs.SetInt("repairValue", repairValue);
        PlayerPrefs.SetInt("currentXP", (int)levelSlider.value);
        PlayerPrefs.SetInt("totalXP", (int)levelSlider.maxValue);
        if (vehicles[0].activeSelf)
        {
            PlayerPrefs.SetInt("v2_health", (int)healthSlider.maxValue);
            PlayerPrefs.SetInt("v2_currenthealth", (int)healthSlider.value);
            PlayerPrefs.SetInt("v2_fuel", (int)fuelSlider.maxValue);
            PlayerPrefs.SetInt("v2_currentfuel", (int)fuelSlider.value);
        }
        else if (vehicles[1].activeSelf)
        {
            PlayerPrefs.SetInt("v3_health", (int)healthSlider.maxValue);
            PlayerPrefs.SetInt("v3_currenthealth", (int)healthSlider.value);
            PlayerPrefs.SetInt("v3_fuel", (int)fuelSlider.maxValue);
            PlayerPrefs.SetInt("v3_currentfuel", (int)fuelSlider.value);
        }

    }

    void LevelSystem()
    {
        levelSlider.value = ls.currentXP;
        XpText.text = "XP Earned: " + ls.currentXP;
        LevelText.text = "Level: " + ls.currentLevel;
        levelSlider.maxValue = ls.totalXP;
    }

    void EndGame()
    {
        PlayerPrefs.SetInt("isNewGame", 1);
        SceneManager.LoadScene(0);
    }

    void EndForDying()
    {
        PlayerPrefs.SetInt("isNewGame", 0);
        SceneManager.LoadScene(0);
    }

    void LoadPP()
    {
        if (vehicleID == 0)
        {
            vehicles[0].GetComponent<MSVehicleControllerFree>()._vehicleTorque.engineTorque = PlayerPrefs.GetInt("v2_torque");
            vehicles[0].GetComponent<MSVehicleControllerFree>()._vehicleTorque.maxVelocityKMh = PlayerPrefs.GetInt("v2_speed");
            fuelSlider.maxValue = PlayerPrefs.GetInt("v2_fuel");
            healthSlider.maxValue = PlayerPrefs.GetInt("v2_health");
        }


        else if (vehicleID == 1)
        {
            vehicles[1].GetComponent<MSVehicleControllerFree>()._vehicleTorque.engineTorque = PlayerPrefs.GetInt("v3_torque");
            vehicles[1].GetComponent<MSVehicleControllerFree>()._vehicleTorque.maxVelocityKMh = PlayerPrefs.GetInt("v3_speed");
            fuelSlider.maxValue = PlayerPrefs.GetInt("v3_fuel");
            healthSlider.maxValue = PlayerPrefs.GetInt("v3_health");
        }
    }


    private void InitVehicle(){
        for(int i =0; i < vehicles.Length; i++){
            if (i == vehicleID)
                vehicles[i].SetActive(true);

            else
                vehicles[i].SetActive(false);
        }
    }

    void RepairSystem()
    {
        repairText.text = "Repair: " + repairValue;

        if (healthSlider.value <= 10)
            vehicleCode.theEngineIsRunning = false;
        if (vehicleCode.theEngineIsRunning == false && Input.GetKeyDown(KeyCode.Z) && repairValue > 0)
        {
            repairValue -= 1;
            Invoke("Repair", 0.5f);
        }
    }

    void CoinSystem()
    {
        coinText.text = "Coins: "+coinValue;
    }

    void FuelSystem()
    {
        kmhText.text = (int)vehicleCode.KMh + " kmh";
        fuelText.text = "Fuel: " + fuelValue;
        if (vehicleCode.isInsideTheCar && Time.timeScale == 1.0f)
        {
            fuelSlider.gameObject.SetActive(true);
            
            gearTxt.text = vehicleCode.currentGear + "";
            if (fuelSlider.value <= 0.1)
                vehicleCode.theEngineIsRunning = false;
            if (vehicleCode.KMh < 30)
                fuelSlider.value -= (vehicleCode.KMh / fuelDecreaseValue) * Time.deltaTime;
            else if (vehicleCode.KMh > 30)
                fuelSlider.value -= (Mathf.Pow(vehicleCode.KMh / fuelDecreaseValue, 2)) * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.X) && fuelValue > 0)
            {
                fuelValue -= 1;
                Invoke("Refill", 0.5f);
            }
        }
    }

    void Repair()
    {
        healthSlider.value += 50;
    }

    void Refill()
    {
        fuelSlider.value += 50;
    }

    void ChangeCanvasCam()
    {
        if (Camera.main.name == "Camera")
            speedUI.gameObject.SetActive(false);
        else
            speedUI.gameObject.SetActive(true);
    }
    void pauseMenuPause()
    {
        Pause = true;
        pauseMenuPanel.SetActive(true);

    }
    public void pauseMenuResume()
    {
        Pause = false;
        pauseMenuPanel.SetActive(false);
    }
    public void pauseMenuQuit()
    {
        Pause = false;
        pauseMenuPanel.gameObject.SetActive(false);
    }

    void Manager()
    {
        FuelSystem();
        CoinSystem();
        RepairSystem();
        ChangeCanvasCam();
        PopUpMessage();
        LevelSystem();
    }

    /*
    IEnumerator PopUpMessageHelper(string message)
    {
        popUpMsg.SetActive(false);
        popUpMsg.transform.DOScale(0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        popUpMsg.SetActive(true);

        Text enterText = popUpMsg.transform.Find("Text").GetComponent<Text>();
        enterText.text = message;

        popUpMsg.transform.DOScale(1, 1);
        yield return new WaitForSeconds(2.5f);
        popUpMsg.transform.DOScale(0, 1);
        yield return new WaitForSeconds(1);
        popUpMsg.SetActive(false);

    }

    void PopUpWhenVehicleNear(){
        currentDistanceTemp = Vector3.Distance(player.transform.position, vehicleCode.doorPosition[0].transform.position);
        for (int x = 0; x < vehicleCode.doorPosition.Length; x++){
            proximityDistanceTemp = Vector3.Distance(player.transform.position, vehicleCode.doorPosition[x].transform.position);
            if (proximityDistanceTemp < currentDistanceTemp)
                currentDistanceTemp = proximityDistanceTemp;
        }

        if (currentDistanceTemp < minDistance && !vehicleCode.isInsideTheCar)
            PopUpMessageHelper("Press '" + controls.enterEndExit + "' to enter the vehicle");
    }
    */
    void PopUpMessage()
    {
        Text enterText = popUpMsg.GetComponentInChildren<Text>();
        float currentDistanceGarage = Vector3.Distance(player.transform.position, ovm.transform.position);
        currentDistanceTemp = Vector3.Distance(player.transform.position, vehicleCode.doorPosition[0].transform.position);
        
        for (int x = 0; x < vehicleCode.doorPosition.Length; x++)
        {
            proximityDistanceTemp = Vector3.Distance(player.transform.position, vehicleCode.doorPosition[x].transform.position);
            if (proximityDistanceTemp < currentDistanceTemp)
                currentDistanceTemp = proximityDistanceTemp;
        }

        if (currentDistanceTemp < minDistance && !vehicleCode.isInsideTheCar)
        {
            popUpMsg.SetActive(true);
            enterText.text = "Press '" + controls.enterEndExit + "' to enter the vehicle";
        }

        else if (fuelSlider.value < 10 && vehicleCode.isInsideTheCar && fuelValue > 0)
        {
            popUpMsg.SetActive(true);
            enterText.text = "Fuel ULTRA low! Press X to refill";
        }

        else if (fuelSlider.value < 35 && vehicleCode.isInsideTheCar && fuelValue == 0)
        {
            popUpMsg.SetActive(true);
            enterText.text = "Low Fuel! You better find some or visit the local petrol station!";
        }

        else if (currentDistanceGarage < 15)
        {
            popUpMsg.SetActive(true);
            enterText.text = "Go to garage to open vehicle shop!";
        }

        else if (insidePetrolStation == true && player.activeSelf == false)
        {
            popUpMsg.SetActive(true);
            enterText.text = "To purchase fuel, please leave your vehicle";
        }
        else if (insidePetrolStation == true && player.activeSelf == true && fuelSlider.value == fuelSlider.maxValue)
        {
            popUpMsg.SetActive(true);
            enterText.text = "Press Enter to buy fuel cans for £5.";
        }
        else if (insidePetrolStation == true && player.activeSelf == true && fuelSlider.value != fuelSlider.maxValue)
        {
            popUpMsg.SetActive(true);
            enterText.text = "Press Enter to buy fuel for £5.";
        }
        else if (vehicleCode.isInsideTheCar && healthSlider.value < 10)
        {
            popUpMsg.SetActive(true);
            enterText.text = "Your vehicle is damaged, please press Z to repair it!";
        }
        else if(ls.finished())
        {
            popUpMsg.SetActive(true);
            enterText.text = "YOU WON!";
            Invoke("EndGame", 10.0f);
        }
        else if (player.GetComponent<MSFPSControllerFree>().ishit)
        {
            popUpMsg.SetActive(true);
            enterText.text = "YOU LOST!";
            Invoke("EndForDying", 3.0f);
        }
        else
        {
            popUpMsg.SetActive(false);
        }

       

    }


    //END ADDONS


    void Awake () {

        error = false;
		CheckEqualKeyCodes ();
		MSSceneControllerFree[] sceneControllers = FindObjectsOfType(typeof(MSSceneControllerFree)) as MSSceneControllerFree[];
		if (sceneControllers.Length > 1) {
			Debug.LogError ("Only one controller is allowed per scene, otherwise the controllers would conflict with each other.");
			error = true;
			for (int x = 0; x < sceneControllers.Length; x++) {
				sceneControllers [x].gameObject.SetActive (false);
			}
		}
		if (startingVehicle >= vehicles.Length) {
			error = true;
			Debug.LogError ("Vehicle selected to start does not exist in the 'vehicles' list");
		}
		if (vehicles.Length == 0) {
			error = true;
			Debug.LogError ("There is no vehicle in the scene or no vehicle has been associated with the controller.");
		}
		for (int x = 0; x < vehicles.Length; x++) {
			if (vehicles [x]) {
				if (!vehicles [x].GetComponent<MSVehicleControllerFree> ()) {
					error = true;
					Debug.LogError ("The vehicle associated with the index " + x + " does not have the 'MSVehicleController' component. So it will be disabled.");
				}
			}else{
				error = true;
				Debug.LogError ("No vehicle was associated with the index " + x + " of the vehicle list.");
			}
		}
		if (error) {
			for (int x = 0; x < vehicles.Length; x++) {
				if (vehicles [x]) {
					MSVehicleControllerFree component = vehicles [x].GetComponent<MSVehicleControllerFree> ();
					if (component) {
						component.disableVehicle = true;
					}
					vehicles [x].SetActive (false);
				}
			}
			return;
		}
		else {
			//UI transform.find
			cameraMobileButton = transform.Find ("Canvas/ChangeCamerasB").GetComponent<Button> ();
			enterAndExitButton = transform.Find ("Canvas/EnterAndExitB").GetComponent<Button> ();

			buttonLeft = transform.Find ("Canvas/MSButtonLeft").GetComponent<MSButtonFree> ();
			buttonRight = transform.Find ("Canvas/MSButtonRight").GetComponent<MSButtonFree> ();
			buttonUp = transform.Find ("Canvas/MSButtonUp").GetComponent<MSButtonFree> ();
			buttonDown = transform.Find ("Canvas/MSButtonDown").GetComponent<MSButtonFree> ();
			joystickCamera = transform.Find ("Canvas/MSJoystickCamera").GetComponent<JoystickFree> ();

			//end transform.find

			if (nextVehicle) {
				nextVehicle.onClick = new Button.ButtonClickedEvent ();
				nextVehicle.onClick.AddListener (() => NextVehicle ());
			}
			if (previousVehicle) {
				previousVehicle.onClick = new Button.ButtonClickedEvent ();
				previousVehicle.onClick.AddListener (() => PreviousVehicle ());
			}

			if (cameraMobileButton) {
				cameraMobileButton.onClick = new Button.ButtonClickedEvent ();
				cameraMobileButton.onClick.AddListener (() => Mobile_CameraInput ());
			}
			if (enterAndExitButton) {
				enterAndExitButton.onClick = new Button.ButtonClickedEvent ();
				enterAndExitButton.onClick.AddListener (() => Mobile_EnterAndExitVehicle ());
			}

			vehicleCode = vehicles [currentVehicle].GetComponent<MSVehicleControllerFree> ();
			EnableOrDisableButtons (vehicleCode.isInsideTheCar);

			Time.timeScale = 1;
			enterAndExitBool = false;
			sceneName = SceneManager.GetActiveScene ().name;
			currentVehicle = startingVehicle;
			//
			for (int x = 0; x < vehicles.Length; x++) {
				if (vehicles [x]) {
					vehicles [x].GetComponent<MSVehicleControllerFree> ().isInsideTheCar = false;
				}
			}
			playerIsNull = false;
			if (player) {
				player.SetActive (false);
			} else {
				playerIsNull = true;
			}
			if (startInPlayer) {
				if (player) {
					player.SetActive (true);
				} else {
					startInPlayer = false;
					if (vehicles.Length > startingVehicle && vehicles [currentVehicle]) {
						vehicles [startingVehicle].GetComponent<MSVehicleControllerFree> ().isInsideTheCar = true;
					}
				}
			} else {
				if (vehicles.Length > startingVehicle && vehicles [currentVehicle]) {
					vehicles [startingVehicle].GetComponent<MSVehicleControllerFree> ().isInsideTheCar = true;
				}
			}
		}
	}

	void CheckEqualKeyCodes(){
		var type = typeof(ControlsFree);
		var fields = type.GetFields();
		var values = (from field in fields
			where field.FieldType == typeof(KeyCode)
			select (KeyCode)field.GetValue(controls)).ToArray();

		foreach (var value in values) {
			if (Array.FindAll (values, (a) => {
				return a == value;
			}).Length > 1) {
				Debug.LogError ("There are similar commands in the 'controls' list. Use different keys for each command.");
				error = true;
			}
		}
	}

	void Update () {
		if (!error) {
			#region customizeInputsValues
			switch (selectControls) {
			case ControlTypeFree.mobileButton:
				if(buttonLeft && buttonRight){
					MSbuttonHorizontal = -buttonLeft.buttonInput+buttonRight.buttonInput;
				}
				if(buttonUp && buttonDown){
					MSbuttonVertical = -buttonDown.buttonInput+ buttonUp.buttonInput;
				}
				if(joystickCamera){
					mouseXInput = joystickCamera.joystickHorizontal;
					mouseYInput = joystickCamera.joystickVertical;
				}
				verticalInput = MSbuttonVertical;
				horizontalInput = MSbuttonHorizontal;
				mouseScrollWheelInput = Input.GetAxis (_mouseScrollWheelInput);
				break;
			case ControlTypeFree.windows:
				verticalInput = Input.GetAxis (_verticalInput);
				horizontalInput = Input.GetAxis (_horizontalInput);
				mouseXInput = Input.GetAxis (_mouseXInput);
				mouseYInput = Input.GetAxis (_mouseYInput);
				mouseScrollWheelInput = Input.GetAxis (_mouseScrollWheelInput);
				break;
			}
			#endregion

			vehicleCode = vehicles [currentVehicle].GetComponent<MSVehicleControllerFree> ();
			EnableOrDisableButtons (vehicleCode.isInsideTheCar);



			if (Input.GetKeyDown (controls.reloadScene) && controls.enable_reloadScene_Input) {
				SceneManager.LoadScene (sceneName);
			}

            if (Input.GetKeyDown(controls.pause) && controls.enable_pause_Input)
            {
                pause = !pause;
            }
            if (pause)
            {
                pauseMenuPause();
                Time.timeScale = 0.0f;
                Cursor.visible = true;
            }
            else
            {
                pauseMenuResume();
                Time.timeScale = 1.0f;
                Cursor.visible = false;
            }

            if ((Input.GetKeyDown (controls.enterEndExit)||enterAndExitBool) && !blockedInteraction && player && controls.enable_enterEndExit_Input) {
				if (vehicles.Length <= 1) {
					if (vehicleCode.isInsideTheCar) {
						vehicleCode.ExitTheVehicle ();
						if (player) {
							player.SetActive (true);
							if (vehicleCode.doorPosition[0].transform.position != vehicles [currentVehicle].transform.position) {
								player.transform.position = vehicleCode.doorPosition[0].transform.position;
							} else {
								player.transform.position = vehicleCode.doorPosition[0].transform.position + Vector3.up * 3.0f;
							}
						}
						blockedInteraction = true;
						enterAndExitBool = false;
						StartCoroutine ("WaitToInteract");
					} else {
						currentDistanceTemp = Vector3.Distance (player.transform.position, vehicleCode.doorPosition[0].transform.position);
						for (int x = 0; x < vehicleCode.doorPosition.Length; x++) {
							proximityDistanceTemp = Vector3.Distance (player.transform.position, vehicleCode.doorPosition [x].transform.position);
							if (proximityDistanceTemp < currentDistanceTemp) {
								currentDistanceTemp = proximityDistanceTemp;
							}
						}
						if (currentDistanceTemp < minDistance) {

							vehicleCode.EnterInVehicle ();
							if (player) {
								player.SetActive (false);
							}
							blockedInteraction = true;
							enterAndExitBool = false;
							StartCoroutine ("WaitToInteract");
						} else {
							enterAndExitBool = false;
						}
					}
				} else {
					proximityObjectIndex = 0;
					proximityDoorIndex = 0;
					for (int x = 0; x < vehicles.Length; x++) {
						controllerTemp = vehicles [x].GetComponent<MSVehicleControllerFree> ();

						for (int y = 0; y < controllerTemp.doorPosition.Length; y++) {
							currentDistanceTemp = Vector3.Distance (player.transform.position, controllerTemp.doorPosition[y].transform.position);
							proximityDistanceTemp = Vector3.Distance (player.transform.position, vehicles [proximityObjectIndex].GetComponent<MSVehicleControllerFree> ().doorPosition[proximityDoorIndex].transform.position);
							if (currentDistanceTemp < proximityDistanceTemp) {
								proximityObjectIndex = x;
								proximityDoorIndex = y;
							}
						}

					}
					
					if (vehicleCode.isInsideTheCar) {
						vehicleCode.ExitTheVehicle ();
						if (player) {
							player.SetActive (true);
							if (vehicleCode.doorPosition[0].transform.position != vehicles [currentVehicle].transform.position) {
								player.transform.position = vehicleCode.doorPosition[0].transform.position;
							} else {
								player.transform.position = vehicleCode.doorPosition[0].transform.position + Vector3.up * 3.0f;
							}
						}
						blockedInteraction = true;
						enterAndExitBool = false;
						StartCoroutine ("WaitToInteract");
					} else {
						controllerTemp = vehicles [proximityObjectIndex].GetComponent<MSVehicleControllerFree> ();
						proximityDistanceTemp = Vector3.Distance (player.transform.position, controllerTemp.doorPosition[0].transform.position);
						for (int x = 0; x < controllerTemp.doorPosition.Length; x++) {
							currentDistanceTemp = Vector3.Distance (player.transform.position, controllerTemp.doorPosition [x].transform.position);
							if (currentDistanceTemp < proximityDistanceTemp) {
								proximityDistanceTemp = currentDistanceTemp;
							}
						}
						if (proximityDistanceTemp < minDistance) {
							currentVehicle = proximityObjectIndex;
							vehicles [currentVehicle].GetComponent<MSVehicleControllerFree> ().EnterInVehicle ();
							if (player) {
								player.SetActive (false);
							}
							blockedInteraction = true;
							enterAndExitBool = false;
							StartCoroutine ("WaitToInteract");
						} else {
							enterAndExitBool = false;
						}
					}
				}
			}
			if (vehicles.Length > 0 && currentVehicle < vehicles.Length && UIVisualizer && vehicleCode) {
				if (vehicleCode.isInsideTheCar) {
					clampGear = Mathf.Clamp (vehicleCode.currentGear, -1, 1);
					if (clampGear == 0) {
						clampGear = 1;
					}

                    //gearText.text =  vehicleCode.currentGear + "GEAR";
                    //mphText.text = (int)(vehicleCode.KMh * 0.621371f * clampGear) + "MPH";
                   

				}
			}

            Manager();
		}
	}



   

	public void PreviousVehicle(){
		if (playerIsNull) {
			if (vehicles.Length > 1) {
				currentVehicle--;
				EnableVehicle (currentVehicle + 1);
			}
		} else {
			if (vehicles.Length > 1 && !player.gameObject.activeInHierarchy) {
				currentVehicle--;
				EnableVehicle (currentVehicle + 1);
			}
		}
	}

	public void NextVehicle(){
		if (playerIsNull) {
			if (vehicles.Length > 1) {
				currentVehicle++;
				EnableVehicle (currentVehicle - 1);
			}
		} else {
			if (vehicles.Length > 1 && !player.gameObject.activeInHierarchy) {
				currentVehicle++;
				EnableVehicle (currentVehicle - 1);
			}
		}
	}

	IEnumerator WaitToInteract(){
		yield return new WaitForSeconds (0.3f);
		blockedInteraction = false;
	}

	void EnableOrDisableButtons(bool insideInCar){
		switch (selectControls) {
		case ControlTypeFree.mobileButton:
			//enter and exit
			if (enterAndExitButton) {
				enterAndExitButton.gameObject.SetActive (true);
			}
			//camera switch e joystick camera
			if (cameraMobileButton) {
				cameraMobileButton.gameObject.SetActive (insideInCar);
			}
			if (joystickCamera) {
				joystickCamera.gameObject.SetActive (insideInCar);
			}
			//move buttons
			if (buttonLeft) {
				buttonLeft.gameObject.SetActive (insideInCar);
			}
			if (buttonRight) {
				buttonRight.gameObject.SetActive (insideInCar);
			}
			if (buttonUp) {
				buttonUp.gameObject.SetActive (insideInCar);
			}
			if (buttonDown) {
				buttonDown.gameObject.SetActive (insideInCar);
			}
			break;
		case ControlTypeFree.windows:
			if (cameraMobileButton) {
				cameraMobileButton.gameObject.SetActive (false);
			}
			if (enterAndExitButton) {
				enterAndExitButton.gameObject.SetActive (false);
			}
			if (joystickCamera) {
				joystickCamera.gameObject.SetActive (false);
			}
			if (buttonLeft) {
				buttonLeft.gameObject.SetActive (false);
			}
			if (buttonRight) {
				buttonRight.gameObject.SetActive (false);
			}
			if (buttonUp) {
				buttonUp.gameObject.SetActive (false);
			}
			if (buttonDown) {
				buttonDown.gameObject.SetActive (false);
			}
			break;
		}
	}

	void EnableVehicle(int index){
		currentVehicle = Mathf.Clamp (currentVehicle, 0, vehicles.Length-1);
		if (index != currentVehicle) {
			if (vehicles [currentVehicle]) {
				//change vehicle
				for (int x = 0; x < vehicles.Length; x++) {
					vehicles [x].GetComponent<MSVehicleControllerFree> ().ExitTheVehicle ();
				}
				vehicles [currentVehicle].GetComponent<MSVehicleControllerFree> ().EnterInVehicle ();
				vehicleCode = vehicles [currentVehicle].GetComponent<MSVehicleControllerFree> ();
			}
		}
	}

	void Mobile_CameraInput(){
		if (!error) {
			if (vehicleCode.isInsideTheCar) {
				vehicleCode.InputsCamerasMobile ();
			}
		}
	}

	void Mobile_EnterAndExitVehicle(){
		if (!error && !enterAndExitBool) {
			enterAndExitBool = true;
		}
	}

}
