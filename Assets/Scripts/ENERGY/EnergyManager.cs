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

    [Header("All Energy is in Watts")]
    [Space(5)]
    //generating
    public float totalEnergyGenerating = 0;
    float totalEnergyGeneratingKW = 0;
    // public float totalVoltsGenerating = 0;
    // public float totalAmpsGenerating = 0;
    
    public float totalEnergyStored = 0;
    // public float totalVoltsStoring = 0;
    // public float totalAmpsStoring = 0;

    public float totalEnergyUsed = 0;

    public float sunAmount = 0;

    public float energyLoss = 0.75f;


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
        sunAmount = 1;
        //indicator for totally generating energy [POSSIBLY DEPRECATED]
        energyGeneratorIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!TimeManager.s.isSimulating)
            return;
        

        //assuming all energy generating objects are connected in series
        totalEnergyGenerating = 0;
        if(generators.Count > 0){
            foreach(Generator obj in generators){
                totalEnergyGenerating += obj.currentEnergy;
            }
        }

        //this depends on the charge controller output?
        totalEnergyStored = 0;
        if(batteries.Count > 0){
            foreach(Battery obj in batteries){
                totalEnergyStored += obj.chargingWattage;
            }
        }


        //indicator for totally generating energy [POSSIBLY DEPRECATED]
        //energyGeneratorIndicator.GetComponent<Lil_Indicator>().UpdateAmount(totalEnergyGenerating);



        totalEnergyUsed = 0;
        foreach(Appliance obj in appliances){
            totalEnergyUsed += obj.currentEnergyUsage;
        }

        //from an array of solar & batteries, take away energy one by one based on appliance needs
        totalEnergyGeneratingKW = totalEnergyGenerating / 1000f;
        UIManager.s.totalEnergyGeneratingText.text = totalEnergyGeneratingKW.ToString("0.00") + " kWhr";
        UIManager.s.totalEnergyStoringText.text = totalEnergyStored.ToString("0.00") + " kWhr";
        UIManager.s.totalEnergyUsingText.text = totalEnergyUsed.ToString("0.00") + "kWhr";
        
    }

    public void AddGenerator(Generator _generator){

        generators.Add(_generator);

        //get median position of all generators
        Vector3 medianPosition = Vector3.zero;
        foreach(Generator obj in generators){
            medianPosition += obj.transform.position;
        }
        medianPosition /= generators.Count;
        
        //indicator for totally generating energy [POSSIBLY DEPRECATED]
        //energyGeneratorIndicator.SetActive(true);
        //energyGeneratorIndicator.transform.position = medianPosition;
        
    }

    public void UpdateSunAmount(float _sunAmount){
        sunAmount = _sunAmount;
        //get the Intensity parameter on the Skybox material
        RenderSettings.skybox.SetFloat("_Intensity", sunAmount);
    }
}
