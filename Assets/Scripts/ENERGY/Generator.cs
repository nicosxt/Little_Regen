using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Generator : EnergyObject
{
    public float currentEnergy = 1;
    public float energyCapacity = 1;

    public Transform chargeIndicator;
    public TextMeshPro energyAmountIndicator;    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        chargeIndicator.localScale = new Vector3(currentEnergy/energyCapacity,1,1);
        energyAmountIndicator.text = currentEnergy.ToString("0.0") + " kw";
        base.Update();
    }

    public override void OnInitiate(){
        Debug.Log("Initiate Energy Generator");
        EnergyManager.s.generators.Add(this);
        base.OnInitiate();
    }
}
