using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeController : EnergyObject
{
    //Charge Controller's jobs are to regulate flow of energy from Generators to Batteries

    protected override void Update()
    {
        //regulate voltage and amperage from Generators to Batteries
        //if voltage is too high, it will be reduced
        //charingAmperage = (solar) totalEnergyGenerating / (battery)chargingVoltage
        //how many hours to charge battery = (battery) ampHours * chargingVoltage / (solar) totalEnergyGenerating


        base.Update();
    }

    public override void OnEnable(){
        InitiateEnergyParameters();
        base.OnEnable();
    }

    void InitiateEnergyParameters(){

    }

    public override void OnInitiate(ObjectInstance _objectInstance){
        //Debug.Log("Initiate")
        base.OnInitiate(_objectInstance);
    }
}
