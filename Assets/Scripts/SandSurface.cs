using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandSurface : MonoBehaviour {

    public MSSceneControllerFree _MSC;
    public MSVehicleControllerFree _MSV;
    private float stdSlipValue;
    private int stdMaxVelocity;
    private float stdEngineTorque;

    void Awake()
    {
        _MSC = GameObject.Find("SceneController").GetComponent<MSSceneControllerFree>();
        _MSV = GameObject.Find("Vehicle5(drift)").GetComponent<MSVehicleControllerFree>();
    }

    void Start()
    {
        stdSlipValue = _MSV._vehicleSettings.improveControl.tireSlipsFactor;
        stdMaxVelocity = _MSV._vehicleTorque.maxVelocityKMh;
        stdEngineTorque = _MSV._vehicleTorque.engineTorque;
    }

    public void OnTriggerEnter()
    {
        _MSV._vehicleSettings.improveControl.tireSlipsFactor = 0.25f;
        _MSV._skidMarks.standardColor = new Color(200, 175, 80);
        _MSV._vehicleTorque.maxVelocityKMh = Mathf.FloorToInt(stdMaxVelocity*0.75f);
        _MSV._vehicleTorque.engineTorque = stdEngineTorque * 0.8f;
    }

    public void OnTriggerExit()
    {
        _MSV._vehicleSettings.improveControl.tireSlipsFactor = stdSlipValue;
        _MSV._skidMarks.standardColor = new Color(0, 0, 0);
        _MSV._vehicleTorque.maxVelocityKMh = stdMaxVelocity;
        _MSV._vehicleTorque.engineTorque = stdEngineTorque;
    }

    void Update()
    {
    }
}
