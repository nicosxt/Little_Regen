using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Generator : EnergyObject
{
    //for now it's just solar
    //only one solar group is needed for the scene
    //so all solar should be next to the original solar

    //later it will be solar, wind, hydro, etc

    public float currentEnergy = 1;
    public float energyCapacity = 1;

    

    //public Transform chargeIndicator;
    //public TextMeshPro energyAmountIndicator;

    // public bool isFirstGenerator; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void OnEnable(){
        base.OnEnable();
    }

    public override void OnInitiate(ObjectInstance _objectInstance){
        // Debug.Log("Initiate Energy Generator");

        EnergyManager.s.AddGenerator(this);
        base.OnInitiate(_objectInstance);
    }
}
