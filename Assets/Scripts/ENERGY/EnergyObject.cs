using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyObject : MonoBehaviour
{
    public string name;

    //reference to ObjectInstance script
    public ObjectInstance objectInstance;

    public GameObject indicator;

    //when object is enabled in the scene (On Hover Mode)
    public virtual void OnEnable(){
        name = gameObject.name;
        ToggleConnectors(false);
        //ToggleIndicators(true);
    }

    //when object is 'registered' in the scene
    public virtual void OnInitiate(ObjectInstance _objectInstance){
        EnergyManager.s.energyObjects.Add(this);
        objectInstance = _objectInstance;
        // ToggleIndicators(false);
        //Debug.Log("Initiate Energy Object");
    }

    public virtual void OnClick(){
        //Debug.Log("Click on Energy Object");
    }

    public virtual void ToggleConnectors(bool _on){

    }

    public virtual void ToggleIndicators(bool _on){
        if(indicator)
            indicator.SetActive(_on);
    }

    // protected virtual void InputingEnergy(float _amount){
    //     energyInput = _amount;
    // }

    // protected virtual void OutputingEnergy(float _amount){
    //     energyOutput = _amount;
    // }

    protected virtual void Update(){
        if(indicator)
            indicator.SetActive(this == Manipulator.s.hoveringEnergyObject);
    }

    // protected void SetPlacingConstraints(bool _hasConstraints){
    //     GetComponent<ObjectInstance>().hasPlacingConstraints = _hasConstraints;
    // }



    // void Update(){
    //     // currentenergyUsage = isOn ? energyUsage : 0f;
    //     // chargeIndicator.SetActive(isOn);
    // }

    // public void ClickOnEnergyObject(){
    //     isOn = !isOn;
    // }

}
