using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelSlider : MonoBehaviour {

    Fuel fuel;
    float remainingFuel;
    float fuelAlert;
    Slider fuelSlider;
    Image fillImage;
  

    // Use this for initialization
    void Start()
    {
        fillImage = fuelSlider.fillRect.GetComponent<Image>();
        fuel = FindObjectOfType<Fuel>();
        fuelSlider = GetComponent<Slider>();
        fuelSlider.maxValue = fuel.GetRemainingFuel();
        fuelAlert = fuel.GetRemainingFuel() * .15f;

    }

    // Update is called once per frame
    void Update()
    {
        DisplayRemainingFuel();
    }

    private void DisplayRemainingFuel()
    {
        remainingFuel = fuel.GetRemainingFuel();
        if (remainingFuel <= fuelAlert) fillImage.color = Color.red;
        fuelSlider.value = remainingFuel;
        
        
    }
}
