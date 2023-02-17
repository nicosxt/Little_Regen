using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Appliance : EnergyObject
{
    public bool isOn = false;
    public bool isConnecteddToEnergySource = false;

    public float currentDischargingAmperage;
    public TextMeshPro dischargeAmpsText;
    public float dischargingAmperage;

    public GameObject applianceIndicator;
    public TextMeshPro applianceIndicatorText;

    [Header("__Circuitry Wizardry__")]
    public Connector connector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        currentDischargingAmperage = isOn ? dischargingAmperage : 0;

        dischargeAmpsText.text = currentDischargingAmperage.ToString("0.0") + "A";
        base.Update();
        
    }

    public override void OnEnable(){
        base.OnEnable();
    }

    protected virtual void OnInitiateAppliance(){
    }

    public override void OnInitiate(ObjectInstance _objectInstance){
        // Debug.Log("Initiate Appliance");
        isOn = false;

        //initiate connectors
        connector.OnInitiate(this, _objectInstance);

        //add self to EnergyManager
        EnergyManager.s.appliances.Add(this);
        OnInitiateAppliance();
        base.OnInitiate(_objectInstance);
    }

    protected virtual void ToggleAppliance(bool _on){}

    void FlipSwitch(){
        isOn = !isOn;
        
        applianceIndicator.SetActive(isOn);
        applianceIndicatorText.text = isOn ? "ON" : "OFF";
        ToggleAppliance(isOn);
    }

    public override void OnClick(){
        FlipSwitch();
    }

    public override void ToggleConnectors(bool _on){
        connector.gameObject.SetActive(_on);
        base.ToggleConnectors(_on);
    }
}
