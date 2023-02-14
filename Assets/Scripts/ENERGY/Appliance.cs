using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Appliance : EnergyObject
{
    public bool isOn = false;
    public bool isConnecteddToEnergySource = false;
    public GameObject chargeIndicator;

    public float currentDischargingAmperage;
    public TextMeshPro dischargeAmpsText;
    public float dischargingAmperage;

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

        dischargeAmpsText.text = currentDischargingAmperage.ToString("F2") + "A";
        base.Update();
        
    }

    public override void OnEnable(){
        base.OnEnable();
    }

    public override void OnInitiate(ObjectInstance _objectInstance){
        // Debug.Log("Initiate Appliance");
        isOn = false;

        //initiate connectors
        connector.OnInitiate(this, _objectInstance);

        chargeIndicator = Instantiate(EnergyManager.s.chargeIndicatorPrefab, transform);
        chargeIndicator.transform.localPosition = new Vector3(0, -0.45f, 0);
        chargeIndicator.SetActive(isOn);
        //add self to EnergyManager
        EnergyManager.s.appliances.Add(this);
        base.OnInitiate(_objectInstance);
    }


    void FlipSwitch(){
        isOn = !isOn;
        chargeIndicator.SetActive(isOn);
    }

    public override void OnClick(){
        FlipSwitch();
    }

    public override void ToggleConnectors(bool _on){
        connector.gameObject.SetActive(_on);
        base.ToggleConnectors(_on);
    }
}
