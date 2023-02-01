using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appliance : EnergyObject
{
    public bool isOn = false;
    public bool isConnecteddToEnergySource = false;

    public float currentEnergyUsage = 0;
    public float energyUsage = 1;


    public GameObject chargeIndicator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        currentEnergyUsage = isOn ? energyUsage : 0;

        // if(isOn){
        //     //taking energy from the system
        // }

        base.Update();
        
    }

    public override void OnEnable(){
        base.OnEnable();
    }

    public override void OnInitiate(ObjectInstance _objectInstance){
        // Debug.Log("Initiate Appliance");
        isOn = true;

        chargeIndicator = Instantiate(EnergyManager.s.chargeIndicatorPrefab, transform);
        chargeIndicator.transform.localPosition = new Vector3(0, -0.45f, 0);

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
}
