using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inverter : EnergyObject
{
    public float inputEnergy;//watthrs
    public float outputVoltage = 110f;
    public float outputAmperage;
    public float inverterLoad = 30f;//watts needed to run the inverter

    public TextMeshPro inputVoltsText, inputAmpsText, outputVoltsText, outputAmpsText;

    [Header("__Circuitry Wizardry__")]
    public Connector positiveInputConnector;
    public Connector negativeInputConnector, outputPlug;

    public override void OnInitiate(ObjectInstance _objectInstance){
        //Debug.Log("Initiate")
        positiveInputConnector.OnInitiate(this, _objectInstance);
        negativeInputConnector.OnInitiate(this, _objectInstance);
        outputPlug.OnInitiate(this, _objectInstance);

        InitiateEnergyParameters();
        EnergyManager.s.inverter = this;
        base.OnInitiate(_objectInstance);
    }

    void InitiateEnergyParameters(){
    }

    protected override void Update()
    {
        base.Update();
    }

    public void UpdateData(){
        inputVoltsText.text = "In: " + EnergyManager.s.batteryOperatingVolts.ToString("F2") + "V";
        inputAmpsText.text = "In: " + EnergyManager.s.batteryCurrentOutputAmps.ToString("F2") + "A";
        outputVoltsText.text = "Out: " + outputVoltage.ToString("F2") + "V";
        outputAmpsText.text = "Out: " + EnergyManager.s.currentACAmps.ToString("F2") + "A";
    }

    public override void ToggleConnectors(bool _on){
        positiveInputConnector.gameObject.SetActive(_on);
        negativeInputConnector.gameObject.SetActive(_on);
        outputPlug.gameObject.SetActive(_on);
        base.ToggleConnectors(_on);
    }
}
