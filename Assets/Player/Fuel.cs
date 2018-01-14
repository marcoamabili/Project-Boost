using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour {

    [SerializeField] float maxFuel = 100;
    [SerializeField] float remainingFuel;
    [SerializeField] float consumptionPerSecond;

    // Use this for initialization
    void Awake ()
    {
        remainingFuel = maxFuel;	
	}

    public void DecreaseFuel()
    {
        remainingFuel -= consumptionPerSecond * Time.deltaTime;
    }

    public float GetRemainingFuel()
    {
        return remainingFuel;
    }

    public void ReloadFuel()
    {
        remainingFuel = maxFuel;
    }



}
