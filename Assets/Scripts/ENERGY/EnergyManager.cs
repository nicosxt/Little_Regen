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
    public float inputPower = 0;
    float inputPowerKW = 0;
    public float inputVolts = 0;
    public float inputAmps = 0;
    
    //Batteries
    public float batteryOperatingVolts = 0;
    //Charge Controller
    public float batteryCurrentInputAmps = 0;//charge controller amp output is battery input
    public float batteryCurrentOutputAmps = 0;//inverter amp discharge
    public float batteryCurrentAmpHours = 0;//accumulative amphours after calculating in and out from batteries
    public float batteryTotalAmpHours = 0;
    public float batteryChargedPercentage = 0;//currentAmphours / totalAmphours


    //Appliances
    public float operatingACVolts = 110f;
    public float currentACAmps = 0;
    public float dischargePower, dischargePowerKW;

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

        if(UIManager.s.currentMode != "Energy" || !TimeManager.s.isSimulating)
            return;

        //🌞 GENERATORS -- POWER GENERATING
        inputVolts = 0;
        if(generators.Count > 0){
            foreach(Generator obj in generators){
                inputVolts += obj.inputVolts;
            }
            //in series, amps are the same
            inputAmps = generators[0].inputAmps;
            inputPower = inputVolts * inputAmps;
        }

        //🔋 BATTERY -- CONFIGURATIONS
        batteryOperatingVolts = 0;
        batteryTotalAmpHours = 0;
        if(batteries.Count > 0){
            foreach(Battery obj in batteries){
                batteryOperatingVolts += obj.operatingVolts;
                batteryTotalAmpHours += obj.totalAmpHours;
            }
        }

        //Check if Generators and Batteries are in scene
        if(batteries.Count == 0 || generators.Count == 0)
            return;

        //🤙 CHARGE CONTROLLER -- battery input amps is the charge controller output amps
        //if(chargeController != null){
        batteryCurrentInputAmps = inputPower / batteryOperatingVolts;
        //}

        //🔌 APPLIANCES
        currentACAmps = 0;
        foreach(Appliance obj in appliances){
            currentACAmps += obj.currentDischargingAmperage;
        }
        //Total power drawn out from the system
        dischargePower = currentACAmps * operatingACVolts;
        
        //amount of amps discharged from battery
        batteryCurrentOutputAmps = (operatingACVolts * currentACAmps) / batteryOperatingVolts;

        //🔋 BATTERY -- ENERGY ACCUMULATION
        if(batteryChargedPercentage <= 100f && batteryChargedPercentage >= 0f){
            batteryCurrentAmpHours += (batteryCurrentInputAmps - batteryCurrentOutputAmps) * TimeManager.s.finalTimeScale;
            batteryChargedPercentage = 100f * batteryCurrentAmpHours / batteryTotalAmpHours;
        }
        
        if(batteryChargedPercentage > 100f){
            batteryChargedPercentage = 100f;
            batteryCurrentAmpHours = batteryTotalAmpHours;
        }
        if(batteryChargedPercentage < 0f){
            batteryChargedPercentage = 0f;
            batteryCurrentAmpHours = 0f;
        }
        
        //from an array of solar & batteries, take away energy one by one based on appliance needs
        inputPowerKW = inputPower / 1000f;
        UIManager.s.totalEnergyGeneratingText.text = inputPowerKW.ToString("0.00") + " kW";
        UIManager.s.totalEnergyStoringText.text = batteryChargedPercentage.ToString("0") + " %";
        dischargePowerKW = dischargePower / 1000f;
        UIManager.s.totalEnergyUsingText.text = dischargePowerKW.ToString("0.00") + "kW";
        
    }

    //PROBABLY DEPRECATED
    public void AddGenerator(Generator _generator){

        generators.Add(_generator);

        //get median position of all generators
        Vector3 medianPosition = Vector3.zero;
        foreach(Generator obj in generators){
            medianPosition += obj.transform.position;
        }
        medianPosition /= generators.Count;
    }

    public void UpdateSunAmount(float _sunAmount){
        sunAmount = _sunAmount;
        //get the Intensity parameter on the Skybox material
        RenderSettings.skybox.SetFloat("_Intensity", sunAmount);
    }
}
