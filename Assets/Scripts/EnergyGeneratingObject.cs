using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyGeneratingObject : MonoBehaviour
{
    public float totalCapacity = 1;//kw/dad/panel
    public float chargingRate = 0.01f;//amount of energy able to receive per hour
    public float currentPowerOutput = 0;

    public bool isCharging = false;

    public TextMeshPro indicator;

    public void OnPlaceEnergyGeneratingObject(){
        isCharging = true;
        EnergyManager.s.energyGeneratingObjects.Add(this);
        
    }

    void OnEnable(){
        indicator.text = currentPowerOutput.ToString("0.00") + " kw";
    }

    void Update(){
        if(isCharging){
            if(currentPowerOutput < totalCapacity){
                currentPowerOutput += chargingRate * Time.deltaTime;
            }else{
                currentPowerOutput = totalCapacity;
            }
            indicator.text = currentPowerOutput.ToString("0.00") + " kw";
        }
    }

}
