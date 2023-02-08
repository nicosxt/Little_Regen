using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : EnergyObject
{
    public float inputEnergy;//watthrs
    public float outputVoltage = 110f;
    public float outputAmperage;
    public float inverterLoad = 30f;//watts needed to run the inverter

    public override void OnInitiate(ObjectInstance _objectInstance){
        //Debug.Log("Initiate")
        InitiateEnergyParameters();
        EnergyManager.s.inverter = this;
        base.OnInitiate(_objectInstance);
    }

    void InitiateEnergyParameters(){
    }
}
