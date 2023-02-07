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


    public float operatingVoltage, operatingAmperage, operatingEnergy;
    
    //when sunshine isn't sufficient, are volts and amps both proportionally affected??
    public float currentEnergy;
    

    // Update is called once per frame
    protected override void Update()
    {
        currentEnergy = operatingEnergy * EnergyManager.s.energyLoss * EnergyManager.s.sunAmount;

        base.Update();
    }

    public override void OnEnable(){
        InitiateEnergyParameters();
        base.OnEnable();
    }

    void InitiateEnergyParameters(){
        operatingVoltage = 30f;//volts
        operatingAmperage = 8f;//amps
        operatingEnergy = operatingVoltage * operatingAmperage;//watts
    }

    public override void OnInitiate(ObjectInstance _objectInstance){
        // Debug.Log("Initiate Energy Generator");

        EnergyManager.s.AddGenerator(this);
        base.OnInitiate(_objectInstance);
    }
}
