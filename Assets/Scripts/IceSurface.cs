using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSurface : MonoBehaviour {

    public MSSceneControllerFree _MSC;
    public MSVehicleControllerFree _MSV;
    private float standardSlipValue;

    void Awake()
    {
        _MSC = GameObject.Find("SceneController").GetComponent<MSSceneControllerFree>();
        _MSV = GameObject.Find("Vehicle5(drift)").GetComponent<MSVehicleControllerFree>();
    }

    void Start()
    {
        standardSlipValue = _MSV._vehicleSettings.improveControl.tireSlipsFactor;
    }

    public void OnTriggerEnter()
    {
        _MSV._vehicleSettings.improveControl.tireSlipsFactor = 0;
        //_MSV._skidMarks.standardColor = new Color(35, 100, 200);
    }

    public void OnTriggerExit()
    {
        _MSV._vehicleSettings.improveControl.tireSlipsFactor = standardSlipValue;
        //_MSV._skidMarks.standardColor = new Color(0, 0, 0);
    }

    void Update()
    {
    }
}
