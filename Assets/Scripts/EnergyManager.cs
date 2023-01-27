using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{

    public static EnergyManager s;
    void Awake() {
        if (s != null && s != this) {
            Destroy(this.gameObject);
        } else {
            s = this;
        }
    }

    public float totalPowerGenerating = 0;
    public float totalPowerUsage = 0;


    public List<EnergyGeneratingObject> energyGeneratingObjects = new List<EnergyGeneratingObject>();
    public List<EnergyObject> energyObjects = new List<EnergyObject>();

    public GameObject chargeIndicatorPrefab;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalPowerGenerating = 0;
        foreach(EnergyGeneratingObject obj in energyGeneratingObjects){
            totalPowerGenerating += obj.currentPowerOutput;
        }

        totalPowerUsage = 0;
        foreach(EnergyObject obj in energyObjects){
            totalPowerUsage += obj.currentPowerUsage;
        }

        UIManager.s.totalEnergyGeneratingText.text = totalPowerGenerating.ToString("0.00") + " kW";
        UIManager.s.totalEnergyUsingText.text = totalPowerUsage.ToString("0.00") + "kW";
        
    }
}
