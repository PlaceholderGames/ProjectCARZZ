using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeCollision : MonoBehaviour {

    private DamageSystem ds;
    private Slider healthSlider;
    private MSVehicleControllerFree vehicle;

    public bool gotHit;
	// Use this for initialization
	void Start () {
        ds = FindObjectOfType<DamageSystem>();
        vehicle = FindObjectOfType<MSVehicleControllerFree>();
        healthSlider = GameObject.Find("healthSlider").GetComponent<Slider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("TreeHit!");
        if (collision.collider.tag == "Vehicle2" || collision.collider.tag == "Vehicle3")
        {
            Debug.Log("TreeHit!");
            gotHit = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Vehicle2" || collision.collider.tag == "Vehicle3")
        {
            ds.RecievedDamage = (10 + (int)Mathf.Sqrt(vehicle.KMh));
            healthSlider.value -= ds.RecievedDamage;
        }
    }
}
