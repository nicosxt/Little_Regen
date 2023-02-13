using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Connector : MonoBehaviour
{
    public EnergyObject energyObject;
    public ObjectInstance objectInstance;

    public float currentVolts, currentAmps, currentPower;
    public bool isPositive = false;
    // Start is called before the first frame update
    public void OnInitiate(EnergyObject eo, ObjectInstance oi){
        energyObject = eo;
        objectInstance = oi;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        Debug.Log("Clicked" + energyObject.name);
    }
}
