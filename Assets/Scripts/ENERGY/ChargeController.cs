using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeController : EnergyObject
{
    //Charge Controller's jobs are to regulate flow of energy from Generators to Batteries

    public float inputVoltageMax, outputAmperageMax;

    public float inputVoltage, inputAmperage, outputVoltage, outputAmperage;

    [Header("__Circuitry Wizardry__")]
    public Connector positiveInputConnector;
    public Connector negativeInputConnector, positiveOutputConnector, negativeOutputConnector;

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
        inputVoltageMax = 150f;//volts
        outputAmperageMax = 80f;//amps
    }

    public override void OnInitiate(ObjectInstance _objectInstance){
        //Debug.Log("Initiate")

        //initiate connectors
        positiveInputConnector.OnInitiate(this, _objectInstance);
        negativeInputConnector.OnInitiate(this, _objectInstance);
        positiveOutputConnector.OnInitiate(this, _objectInstance);
        negativeOutputConnector.OnInitiate(this, _objectInstance);

        InitiateEnergyParameters();
        EnergyManager.s.chargeController = this;
        base.OnInitiate(_objectInstance);
    }
}
