using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Generator : EnergyObject
{


    public float operatingVolts, operatingAmps, operatingPower;
    
    //power input into the system
    public float inputVolts, inputAmps, inputPower;
    public TextMeshPro inputVoltsText, inputAmpsText, inputPowerText;
  
    [Header("__Circuitry Wizardry__")]
    public Connector positiveConnector;
    public Connector negativeConnector;

    // Update is called once per frame
    protected override void Update()
    {
        inputVolts = operatingVolts * EnergyManager.s.generatorEnergyLoss * EnergyManager.s.sunAmount;
        inputAmps = operatingAmps * EnergyManager.s.generatorEnergyLoss * EnergyManager.s.sunAmount;
        inputPower = inputAmps * inputVolts;
        inputVoltsText.text = inputVolts.ToString("F2") + "V";
        inputAmpsText.text = inputAmps.ToString("F2") + "A";
        inputPowerText.text = (inputPower / 1000f).ToString("F2") + "kW";
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
        positiveConnector.OnInitiate(this, _objectInstance);
        negativeConnector.OnInitiate(this, _objectInstance);

        EnergyManager.s.AddGenerator(this);
        base.OnInitiate(_objectInstance);
    }

    public override void ToggleConnectors(bool _on){
        positiveConnector.gameObject.SetActive(_on);
        negativeConnector.gameObject.SetActive(_on);
        base.ToggleConnectors(_on);
    }
}
