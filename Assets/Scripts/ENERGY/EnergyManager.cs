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

    [Header("All Energy is in KiloWatts")]
    [Space(5)]
    public float totalEnergyGenerating = 0;
    public float totalEnergyStoring = 0;
    public float totalEnergyUsage = 0;


    // public List<EnergyGeneratingObject> energyGeneratingObjects = new List<EnergyGeneratingObject>();
    // public List<EnergyObject> energyObjects = new List<EnergyObject>();

    public List<Generator> generators = new List<Generator>();
    public GameObject energyGeneratorIndicator;
    public List<Appliance> appliances = new List<Appliance>();
    public List<Battery> batteries = new List<Battery>();

    public GameObject chargeIndicatorPrefab;
    

    // Start is called before the first frame update
    void Start()
    {
        energyGeneratorIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        totalEnergyGenerating = 0;
        foreach(Generator obj in generators){
            totalEnergyGenerating += obj.currentEnergy;
        }
        energyGeneratorIndicator.GetComponent<Lil_Indicator>().UpdateAmount(totalEnergyGenerating);

        totalEnergyStoring = 0;
        foreach(Battery obj in batteries){
            totalEnergyStoring += obj.currentEnergyStored;
        }

        totalEnergyUsage = 0;
        foreach(Appliance obj in appliances){
            totalEnergyUsage += obj.currentEnergyUsage;
        }

        //from an array of solar & batteries, take away energy one by one based on appliance needs
        

        UIManager.s.totalEnergyGeneratingText.text = totalEnergyGenerating.ToString("0.00") + " kW";
        UIManager.s.totalEnergyStoringText.text = totalEnergyStoring.ToString("0.00") + "kW";
        UIManager.s.totalEnergyUsingText.text = totalEnergyUsage.ToString("0.00") + "kW";
        
    }

    public void AddGenerator(Generator _generator){

        generators.Add(_generator);
        energyGeneratorIndicator.SetActive(true);

        //get median position of all generators
        Vector3 medianPosition = Vector3.zero;
        foreach(Generator obj in generators){
            medianPosition += obj.transform.position;
        }
        medianPosition /= generators.Count;
        energyGeneratorIndicator.transform.position = medianPosition;
        
    }
}
