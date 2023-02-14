using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Battery : EnergyObject
{

    //additional parameters
    public float operatingVolts;//this doesn't change
    public float operatingAmpsMax;
    public float operatingAmpHours;

    public TextMeshPro operatingVoltsText, inputAmpsText, currentPowerText;

    public float inputAmps, currentPower;//current amperage, if larger than max, battery explodes
    
    bool isCharging;

    [Header("__Circuitry Wizardry__")]
    public Connector positiveConnector;
    public Connector negativeConnector;

    public override void OnEnable(){
        base.OnEnable();
    }
    
    public override void OnInitiate(ObjectInstance _objectInstance){
        isCharging = false;

        //initiate connectors
        positiveConnector.OnInitiate(this, _objectInstance);
        negativeConnector.OnInitiate(this, _objectInstance);

        InitiateEnergyParameters();

        operatingVoltsText.text = operatingVolts.ToString("F2") + "V";

        // Debug.Log("Initiate Battery");
        EnergyManager.s.batteries.Add(this);
        base.OnInitiate(_objectInstance);
    }

    void InitiateEnergyParameters(){
        operatingVolts = 14.4f;//volts
        operatingAmpHours = 200f;
        operatingAmpsMax = 100f;//amps

    }

    // Update is called once per frame
    protected override void Update()
    {
        

        base.Update();
    }

    public void UpdateData(float _currentamps){
        inputAmps = _currentamps;
        currentPower = inputAmps * operatingVolts;
        inputAmpsText.text = inputAmps.ToString("F2") + "A";
        currentPowerText.text = (inputAmps * operatingVolts).ToString("F2") + "W";
    }

    public override void ToggleConnectors(bool _on){
        positiveConnector.gameObject.SetActive(_on);
        negativeConnector.gameObject.SetActive(_on);
        base.ToggleConnectors(_on);
    }

}
