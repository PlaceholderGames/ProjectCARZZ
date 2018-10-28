using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceTrigger : MonoBehaviour {

    public MSSceneControllerFree _MSC;
    public MSVehicleControllerFree _MSV;

    void Awake()
    {
        _MSC = GameObject.Find("SceneController").GetComponent<MSSceneControllerFree>();
        _MSV = GameObject.Find("Vehicle5(drift)").GetComponent<MSVehicleControllerFree>();
    }

    public void OnTriggerEnter()
    {
        _MSV._vehicleSettings.improveControl.tireSlipsFactor = 0;
        _MSV._skidMarks.standardColor = new Color(35, 100, 200);
    }

    public void OnTriggerExit()
    {
        _MSV._vehicleSettings.improveControl.tireSlipsFactor = 0.85f;
        _MSV._skidMarks.standardColor = new Color(0, 0, 0);
    }

    void Update()
    {
    }
}
