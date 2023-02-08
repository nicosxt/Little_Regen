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
    //Generators
    public float totalEnergyGenerating = 0;
    float totalEnergyGeneratingKW = 0;
    public float totalVoltsGenerating = 0;
    public float totalAmpsGenerating = 0;
    
    //Batteries
    public float batteryVoltage = 0;
    public float currentBatteryAmpCharge, currentBatteryAmpDischarge;
    public float currentBatteryEnergyDelta = 0;//charge - discharge
    public float currentBatteryAmpHours, totalBatteryAmpHours = 0;
    public float batteryChargedPercentage = 0;

    //Appliances
    public float totalApplianceLoad = 0;//watts, before inverter converts it to AC
    public float currentACAmps;
    public float currentACEnergyDischarge;
    public float currentACEnergyDischargeKW;
    public float ACVolts = 110f;
    
    public float currentDCAmps;


    // public float totalVoltsStoring = 0;
    // public float totalAmpsStoring = 0;

    public float sunAmount = 0;

    public float generatorEnergyLoss = 0.75f;


    // public List<EnergyGeneratingObject> energyGeneratingObjects = new List<EnergyGeneratingObject>();
    // public List<EnergyObject> energyObjects = new List<EnergyObject>();

    public List<Generator> generators = new List<Generator>();
    public GameObject energyGeneratorIndicator;
    public List<Appliance> appliances = new List<Appliance>();
    public List<Battery> batteries = new List<Battery>();
    //public List<ChargeController> chargeControllers = new List<ChargeController>();
    public ChargeController chargeController;
    public Inverter inverter;

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
        

        //Generators
        //assuming all energy generating objects are connected in series
        totalEnergyGenerating = 0;
        totalVoltsGenerating = 0;
        totalAmpsGenerating = 0;
        if(generators.Count > 0){
            foreach(Generator obj in generators){
                totalEnergyGenerating += obj.currentEnergy;
                totalVoltsGenerating += obj.currentVoltage;
            }
            if(generators.Count > 0)
                totalAmpsGenerating = generators[0].currentAmperage;
        }

        //Get total voltage from Batteries
        //this depends on the charge controller output?
        batteryVoltage = 0;
        totalBatteryAmpHours = 0;
        if(batteries.Count > 0){
            foreach(Battery obj in batteries){
                batteryVoltage += obj.chargingVoltage;
                totalBatteryAmpHours += obj.chargingAmpHours;
            }
        }

        //⚡️🤙 Charge Controller Magic ⚡️🤙
        if(chargeController != null){
            chargeController.inputVoltage = totalVoltsGenerating;
            chargeController.inputAmperage = totalAmpsGenerating;
            chargeController.outputVoltage = batteryVoltage;
            chargeController.outputAmperage = totalVoltsGenerating * totalAmpsGenerating / batteryVoltage;
        }

        //current battery amperage charge is the charge controller output amperage
        currentBatteryAmpCharge = chargeController.outputAmperage;

        //calculating total accumulating amperage charged into the battery
        if(currentBatteryAmpHours < totalBatteryAmpHours){
            currentBatteryAmpHours += chargeController.outputAmperage * Time.deltaTime * TimeManager.s.timeScale * TimeManager.s.timeMultiplier / 60f;
        }else{
            currentBatteryAmpHours = totalBatteryAmpHours;
        }

        //⚡️🤙 Inverter Magic ⚡️🤙
        if(inverter != null){
            inverter.inputEnergy = batteryVoltage;
        }

        //Appliances
        currentACAmps = 0;
        foreach(Appliance obj in appliances){
            currentACAmps += obj.currentDischargingAmperage;
        }
        currentACEnergyDischarge = currentACAmps * ACVolts;

        //Get amount of amps needed from the inverter
        //minus power of battery from inverter load
        if(inverter != null){
            currentDCAmps = currentACEnergyDischarge / batteryVoltage;
            currentBatteryAmpDischarge = currentDCAmps;
        }

        currentBatteryEnergyDelta = currentBatteryAmpCharge * batteryVoltage - currentACEnergyDischarge;
        currentBatteryAmpHours += currentBatteryEnergyDelta/batteryVoltage;

        //indicator for totally generating energy [POSSIBLY DEPRECATED]
        //energyGeneratorIndicator.GetComponent<Lil_Indicator>().UpdateAmount(totalEnergyGenerating);



        //Divide the load into individual batteries
        if(batteries.Count > 0){
            foreach(Battery obj in batteries){
                obj.currentAmperage = currentBatteryAmpHours / batteries.Count;
                obj.currentCapacity = obj.currentAmperage * obj.chargingVoltage;
            }
        }
        batteryChargedPercentage = 100f * currentBatteryAmpHours / totalBatteryAmpHours;




        //from an array of solar & batteries, take away energy one by one based on appliance needs
        totalEnergyGeneratingKW = totalEnergyGenerating / 1000f;
        UIManager.s.totalEnergyGeneratingText.text = totalEnergyGeneratingKW.ToString("0.00") + " kW";
        UIManager.s.totalEnergyStoringText.text = batteryChargedPercentage.ToString("0") + " %";
        currentACEnergyDischargeKW = currentACEnergyDischarge / 1000f;
        UIManager.s.totalEnergyUsingText.text = currentACEnergyDischargeKW.ToString("0.00") + "kW";
        
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
