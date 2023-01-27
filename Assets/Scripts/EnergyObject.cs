using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyObject : MonoBehaviour
{
    public string name;
    public float powerUsage = 1f;// K Watts
    public float currentPowerUsage;

    public bool isOn;

    public GameObject chargeIndicator;
    
    public void OnInitiateEnergyObject(){
        isOn = true;
        name = gameObject.name;
        EnergyManager.s.energyObjects.Add(this);
        chargeIndicator = Instantiate(EnergyManager.s.chargeIndicatorPrefab, transform);
        chargeIndicator.transform.localPosition = new Vector3(0, -0.45f, 0);
        //chargeIndicator.SetActive(true);
    }

    void Update(){
        currentPowerUsage = isOn ? powerUsage : 0f;
        chargeIndicator.SetActive(isOn);
    }

    public void ClickOnEnergyObject(){
        isOn = !isOn;
    }

}
