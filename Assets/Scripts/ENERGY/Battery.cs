using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Battery : EnergyObject
{
    //public float currentEnergyInput = 1;//energy input from chargers
    public float currentEnergyStored = 0;//amount of energy stored
    public float energyCapacity = 1;
    public float chargingRate = 0.002f;

    public Transform batteryIndicator;
    public TextMeshPro energyAmountIndicator;

    bool isCharging;

    public bool isFirstBattery;

    public override void OnEnable(){
        currentEnergyStored = 0;
        base.OnEnable();
    }
    
    public override void OnInitiate(ObjectInstance _objectInstance){
        isCharging = true;
        // Debug.Log("Initiate Battery");
        EnergyManager.s.batteries.Add(this);
        base.OnInitiate(_objectInstance);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(isCharging){
            if(currentEnergyStored < energyCapacity){
                currentEnergyStored += chargingRate * Time.deltaTime;
            }else{
                currentEnergyStored = energyCapacity;
            }
        }

        batteryIndicator.localScale = new Vector3(currentEnergyStored/energyCapacity, 1, 1);
        energyAmountIndicator.text = currentEnergyStored.ToString("0.0") + " kw";
        base.Update();
    }

}
