using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelSystem : MonoBehaviour {

    MSVehicleControllerFree mS;
    [SerializeField] private Slider slider;
  

    public void FuelManager()
    {
        if (mS)
        {
            slider.gameObject.SetActive(true);
        }
        else
            slider.gameObject.SetActive(false);
    }

    private void Update()
    {
        FuelManager();
    }
}
