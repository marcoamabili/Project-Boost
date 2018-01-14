using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelDisplay : MonoBehaviour {

    Fuel fuel;
    Text fuelText;
    float remainingFuel;

	// Use this for initialization
	void Start ()
    {
        fuel = FindObjectOfType<Fuel>();
        fuelText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        DisplayRemainingFuel();
    }

    private void DisplayRemainingFuel()
    {
        remainingFuel = fuel.GetRemainingFuel();
        fuelText.text = "Fuel remaining: " + remainingFuel.ToString();
    }
}
