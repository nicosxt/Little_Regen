using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Battery : EnergyObject
{
    //[PROOBABLY DELETE THIS]
    public GameObject indicator;
    public Transform batteryIndicator;
    public TextMeshPro energyAmountIndicator;
    public bool isFirstBattery;

    //additional parameters
    public float chargingVoltage;//this doesn't change
    public float chargingAmperageMax;
    public float chargingAmpHours;
    public float totalCapacity;//watts

    public float currentAmperage;//current amperage, if larger than max, battery explodes
    public float currentCapacity;
    
    bool isCharging;


    public override void OnEnable(){
        indicator.SetActive(false);
        base.OnEnable();
    }
    
    public override void OnInitiate(ObjectInstance _objectInstance){
        isCharging = false;
        InitiateEnergyParameters();
        // Debug.Log("Initiate Battery");
        EnergyManager.s.batteries.Add(this);
        base.OnInitiate(_objectInstance);
    }

    void InitiateEnergyParameters(){
        chargingVoltage = 14.4f;//volts
        chargingAmpHours = 200f;
        totalCapacity = chargingVoltage * chargingAmpHours;//watts
        chargingAmperageMax = 100f;//amps

    }

    // Update is called once per frame
    protected override void Update()
    {


        //Update Indicator [POSSIBLY DEPRECATED]
        // batteryIndicator.localScale = new Vector3(currentEnergyStored/energyCapacity, 1, 1);
        // energyAmountIndicator.text = currentEnergyStored.ToString("0.0") + " kw";
        base.Update();
    }

}
