using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnergyManager : MonoBehaviour
{

    public static EnergyManager s;
    // InputActions inputActions;
    void Awake() {
        if (s != null && s != this) {
            Destroy(this.gameObject);
        } else {
            s = this;
        }
        // inputActions = new InputActions();
    }


    [Header("__Generators__")]
    public float inputPower = 0;
    float inputPowerKW = 0;
    public float inputVolts = 0;
    public float inputAmps = 0;
    public float generatorEnergyLoss = 0.75f;
    public List<Generator> generators = new List<Generator>();
    
    [Header("__Batteries__")]
    public float batteryOperatingVolts = 0;
    //Charge Controller
    public float batteryCurrentInputAmps = 0;//charge controller amp output is battery input
    public float batteryCurrentOutputAmps = 0;//inverter amp discharge
    public float batteryCurrentAmpHours = 0;//accumulative amphours after calculating in and out from batteries
    public float batteryTotalAmpHours = 0;
    public float batteryChargedPercentage = 0;//currentAmphours / totalAmphours
    public List<Battery> batteries = new List<Battery>();


    //Appliances
    [Header("__Appliances__")]
    public float operatingACVolts = 110f;
    public float currentACAmps = 0;
    public float dischargePower, dischargePowerKW;
    public List<Appliance> appliances = new List<Appliance>();
    public GameObject applianceIndicatorPrefab;

    [Header("__EnergyObjects__")]
    public List<EnergyObject> energyObjects = new List<EnergyObject>();

    [Header("__Environment__")]
    public float sunAmount = 0;

    [Header("__Charge Controller & Inverter__")]
    public ChargeController chargeController;
    public Inverter inverter;

    [Header("__Circuitry Wizardry__")]
    public GameObject connectorPrefab;
    public GameObject wirePrefab;
    public Connector currentConnector, previousConnector;
    public Color buttonSelectedColor, buttonConnectedColor;


    // Start is called before the first frame update
    void Start()
    {
        UpdateSunAmount(1f);
        //indicator for totally generating energy [POSSIBLY DEPRECATED]
        //energyGeneratorIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateEnergyFlow();

        //Circuitry Logic

    }

    //called in Manipulator
    public void OnClick(){
        //Debug.Log("Click Energy Manager");
        RaycastHit hit;
        if (Physics.Raycast(Manipulator.s.mouseRay, out hit, 1000f, 1 << 9))
        {
            Debug.Log(hit.collider.name);
            if(hit.transform.parent.GetComponent<Connector>() != null){
                hit.transform.parent.GetComponent<Connector>().OnClick();
                currentConnector = hit.transform.parent.GetComponent<Connector>();
                
                if(previousConnector == null && currentConnector != null){
                    currentConnector.SetState("selected");
                    previousConnector = currentConnector;
                }else if(previousConnector != currentConnector){
                    //Connect 2 connectors
                    GameObject newWire = Instantiate(wirePrefab, previousConnector.transform.position, Quaternion.identity, transform);
                    newWire.GetComponent<Wire>().connectorFrom = previousConnector;
                    newWire.GetComponent<Wire>().connectorTo = currentConnector;
                    newWire.GetComponent<Wire>().SetWire();
                    previousConnector.SetState("connected");
                    currentConnector.SetState("connected");
                    previousConnector = null;
                    currentConnector = null;
                }
            }
        }
    }

    void CalculateEnergyFlow(){

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
        UIManager.s.UpdateSolarStats(generators.Count, inputVolts, inputAmps, inputPower);

        if(batteries.Count > 0){

            //🔋 BATTERY -- CONFIGURATIONS
            batteryOperatingVolts = 0;
            batteryTotalAmpHours = 0;

            foreach(Battery obj in batteries){
                batteryOperatingVolts += obj.operatingVolts;
                batteryTotalAmpHours += obj.operatingAmpHours;
            }

            //🤙 CHARGE CONTROLLER -- battery input amps is the charge controller output amps
            batteryCurrentInputAmps = inputPower / batteryOperatingVolts;
            //UpdateChargeController
            if(chargeController != null)
               chargeController.UpdateData();
            
        }


        //🔌 APPLIANCES
        currentACAmps = 0;
        foreach(Appliance obj in appliances){
            currentACAmps += obj.currentDischargingAmperage;
        }
        //Total power drawn out from the system
        dischargePower = currentACAmps * operatingACVolts;
        UIManager.s.UpdateAppliancesStats(appliances.Count, currentACAmps, operatingACVolts);
        
        //amount of amps discharged from battery
        batteryCurrentOutputAmps = (batteryOperatingVolts == 0) ? 0 : (operatingACVolts * currentACAmps) / batteryOperatingVolts;

        //🔋 BATTERY -- ENERGY ACCUMULATION
        if(batteryChargedPercentage <= 100f && batteryChargedPercentage >= 0f){
            batteryCurrentAmpHours += (batteryCurrentInputAmps - batteryCurrentOutputAmps) * TimeManager.s.finalTimeScale;
            batteryChargedPercentage = (batteryOperatingVolts == 0) ? 0 : 100f * batteryCurrentAmpHours / batteryTotalAmpHours;
        }
        
        if(batteryChargedPercentage > 100f){
            batteryChargedPercentage = 100f;
            batteryCurrentAmpHours = batteryTotalAmpHours;
        }
        if(batteryChargedPercentage < 0f){
            batteryChargedPercentage = 0f;
            batteryCurrentAmpHours = 0f;
        }
        //updating individual batteries
        if(batteries.Count > 0){
            foreach(Battery obj in batteries){
                obj.UpdateData(batteryCurrentAmpHours / batteries.Count);
            }
        }

        //update inverter
        if(inverter)
            inverter.UpdateData();
        
        //from an array of solar & batteries, take away energy one by one based on appliance needs
        UIManager.s.UpdateBatteryStats(batteries.Count, batteryTotalAmpHours * batteryOperatingVolts, batteryCurrentAmpHours * batteryOperatingVolts, batteryChargedPercentage, batteryOperatingVolts, batteryCurrentInputAmps, batteryCurrentOutputAmps);
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
        RenderSettings.skybox.SetFloat("_Intensity", sunAmount* 1.2f + 0.1f);
    }
}
