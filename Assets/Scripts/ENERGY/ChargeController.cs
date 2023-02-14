using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChargeController : EnergyObject
{
    //Charge Controller's jobs are to regulate flow of energy from Generators to Batteries

    public float inputVoltageMax, outputAmperageMax;

    public float inputVoltage, inputAmperage, outputVoltage, outputAmperage;
    public TextMeshPro inputVoltsText, inputAmpsText, outputVoltsText, outputAmpsText;

    [Header("__Circuitry Wizardry__")]
    public Connector positiveInputConnector;
    public Connector negativeInputConnector, positiveOutputConnector, negativeOutputConnector;

    protected override void Update()
    {
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

    public void UpdateData(){
        inputVoltage = EnergyManager.s.inputVolts;
        inputAmperage = EnergyManager.s.inputAmps;
        outputVoltage = EnergyManager.s.batteryOperatingVolts;
        outputAmperage = EnergyManager.s.batteryCurrentInputAmps;
        
        inputVoltsText.text = "In: " + inputVoltage.ToString("F2") + "V";
        inputAmpsText.text = "In: " + inputAmperage.ToString("F2") + "A";
        outputVoltsText.text = "Out: " + outputVoltage.ToString("F2") + "V";
        outputAmpsText.text = "Out: " + outputAmperage.ToString("F2") + "A";
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

    public override void ToggleConnectors(bool _on){
        positiveInputConnector.gameObject.SetActive(_on);
        negativeInputConnector.gameObject.SetActive(_on);
        positiveOutputConnector.gameObject.SetActive(_on);
        negativeOutputConnector.gameObject.SetActive(_on);
        base.ToggleConnectors(_on);
    }
}
