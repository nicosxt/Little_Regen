using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyGeneratingObject : MonoBehaviour
{
    //Energy Input (Solar, Wind, Hydro)
    public float solarCapacity = 1;
    public float solarChargeAmount = 1;

    //Energy Storage (Battery)
    public float batteryCapacity = 1;//kw/dad/panel
    public float batteryChargeAmount = 0;

    //Charging
    public bool isCharging = false;
    public float batteryChargingRate = 0.001f;//amount of energy able to receive per hour

    //Total Energy
    public float chargeOutput = 0;
    public float chargeWithdrawAmount = 0;//amount of energy withdrawn from total charge every frame
    

    //Indicators
    public TextMeshPro capacityIndicator;
    public Transform batteryIndicator, chargeIndicator;

    public void OnPlaceEnergyGeneratingObject(){
        isCharging = true;
        //EnergyManager.s.energyGeneratingObjects.Add(this);
        
    }

    void OnEnable(){

        capacityIndicator.text = batteryCapacity.ToString("0.00") + " kw";
    }

    void Update(){

        ChargeBattery();

        //withdraw amount of charge
        if(batteryChargeAmount > solarChargeAmount){
            //using battery power
            batteryChargeAmount -= chargeWithdrawAmount * Time.deltaTime;
        }else{
            //using solar
            solarChargeAmount -= chargeWithdrawAmount * Time.deltaTime;
        }

        //assuming solar is always full
        solarChargeAmount = solarCapacity;

        //calculate total charge output
        chargeOutput = (solarChargeAmount > batteryChargeAmount) ? solarChargeAmount : batteryChargeAmount;

        //set visualization
        batteryIndicator.localScale = new Vector3(batteryChargeAmount/batteryCapacity, 1, 1);
        chargeIndicator.localScale = new Vector3(solarChargeAmount/solarCapacity,1,1);
    }

    public void WithdrawCharge(float _amount){
        chargeWithdrawAmount = _amount;
    }

    void ChargeBattery(){
        if(isCharging){
            if(batteryChargeAmount < batteryCapacity){
                batteryChargeAmount += batteryChargingRate * Time.deltaTime;
            }else{
                batteryChargeAmount = batteryCapacity;
            }
        }
    }

}
