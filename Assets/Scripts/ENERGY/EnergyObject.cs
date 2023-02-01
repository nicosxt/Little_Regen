using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyObject : MonoBehaviour
{
    [Header("All Energy is in KiloWatts")]
    public string name;

    //reference to ObjectInstance script
    public ObjectInstance objectInstance;

    public virtual void OnEnable(){
        name = gameObject.name;
    }

    public virtual void OnInitiate(ObjectInstance _objectInstance){
        objectInstance = _objectInstance;
        Debug.Log("Initiate Energy Object");
    }

    public virtual void OnClick(){
        Debug.Log("Click on Energy Object");
    }

    // protected virtual void InputingEnergy(float _amount){
    //     energyInput = _amount;
    // }

    // protected virtual void OutputingEnergy(float _amount){
    //     energyOutput = _amount;
    // }

    protected virtual void Update(){

    }

    // protected void SetPlacingConstraints(bool _hasConstraints){
    //     GetComponent<ObjectInstance>().hasPlacingConstraints = _hasConstraints;
    // }



    // void Update(){
    //     // currentPowerUsage = isOn ? powerUsage : 0f;
    //     // chargeIndicator.SetActive(isOn);
    // }

    // public void ClickOnEnergyObject(){
    //     isOn = !isOn;
    // }

}
