using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyStoringObject : EnergyObject
{
    public float totalEnergyCapacity = 1;//kw
    public float currentEnergy = 0;
    public float storingSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(currentEnergy < totalEnergyCapacity){
        //     currentEnergy += chargingSpeed * Time.deltaTime;
        // }
    }
}
