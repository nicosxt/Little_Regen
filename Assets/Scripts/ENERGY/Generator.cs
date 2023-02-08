using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Generator : EnergyObject
{
    //for now it's just solar
    //only one solar group is needed for the scene
    //so all solar should be next to the original solar
    //later it will be solar, wind, hydro, etc

    

    //public Transform chargeIndicator;
    //public TextMeshPro energyAmountIndicator;
    // public bool isFirstGenerator; 


    public float operatingVolts, operatingAmps, operatingPower;
    
    //power input into the system
    public float inputVolts, inputAmps, inputPower;
    

    // Update is called once per frame
    protected override void Update()
    {
        inputVolts = operatingVolts * EnergyManager.s.generatorEnergyLoss * EnergyManager.s.sunAmount;
        inputAmps = operatingAmps * EnergyManager.s.generatorEnergyLoss * EnergyManager.s.sunAmount;
        inputPower = inputAmps * inputVolts;
        base.Update();
    }

    public override void OnEnable(){
        InitiateEnergyParameters();
        base.OnEnable();
    }

    void InitiateEnergyParameters(){
        operatingVolts = 30f;//volts
        operatingAmps = 8f;//amps
        operatingPower = operatingVolts * operatingAmps;//watts
    }

    public override void OnInitiate(ObjectInstance _objectInstance){
        // Debug.Log("Initiate Energy Generator");

        EnergyManager.s.AddGenerator(this);
        base.OnInitiate(_objectInstance);
    }
}
