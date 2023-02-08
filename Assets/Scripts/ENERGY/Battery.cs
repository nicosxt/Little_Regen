using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Battery : EnergyObject
{
    //additional parameters
    public float operatingVolts;//this doesn't change
    public float operatingAmpsMax;
    public float totalAmpHours;

    public float inputAmps;//current amperage, if larger than max, battery explodes
    
    bool isCharging;


    public override void OnEnable(){
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
        operatingVolts = 14.4f;//volts
        totalAmpHours = 200f;
        operatingAmpsMax = 100f;//amps

    }

    // Update is called once per frame
    protected override void Update()
    {

        base.Update();
    }

}
