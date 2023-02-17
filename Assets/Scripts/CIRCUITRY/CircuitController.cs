using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CircuitController : MonoBehaviour
{
    public TMP_InputField solarAmountSeriesTxt, solarAmountParallelTxt, batteryAmountSeriesTxt, batteryAmountParallelTxt;
    public TextMeshProUGUI solarStatsText, batteryStatsText, chargingTimeText;

    public int solarAmountSeries, solarAmountParallel, batteryAmountSeries, batteryAmountParallel;

    public GameObject solarIconPrefab, batteryIconPrefab;
    public GameObject solarRowPrefab, batteryRowPrefab;
    
    public GameObject solarContainer, batteryContainer;

    //To be changed later
    public float solarOperatingVolts = 30f;
    public float solarOperatingAmps = 8f;
    public float solarOperatingPower = 240f;
    public float batteryOperatingVolts = 14.4f;
    public float batteryOperatingAmpHours = 200f;
    public float batteryOperatingPower = 2880f;
    float chargingTimeHrs;
    public float solarTotalVolts, solarTotalAmps, solarTotalPowerKW;
    public float batteryTotalVolts, batteryTotalAmps, batteryTotalPowerKW;

    // Start is called before the first frame update
    void Start()
    {

        solarAmountParallel = 1;
        batteryAmountParallel = 1;
        solarAmountSeries = 1;
        batteryAmountSeries = 1;

        solarAmountSeriesTxt.onValueChanged.AddListener(delegate {UpdateSolarSeriesValue();});
        solarAmountParallelTxt.onValueChanged.AddListener(delegate {UpdateSolarParallelValue();});
        batteryAmountParallelTxt.onValueChanged.AddListener(delegate {UpdateBatteryParallelValue();});
        batteryAmountSeriesTxt.onValueChanged.AddListener(delegate {UpdateBatterySeriesValue();});

        UpdateSolarAmount();
        UpdateBatteryAmount();
    }

    // Update is called once per frame
    void Update()
    {
        //series += volts
        solarTotalVolts = solarAmountSeries * solarOperatingVolts;
        solarTotalAmps = solarAmountParallel * solarOperatingAmps;
        solarTotalPowerKW = solarTotalVolts * solarTotalAmps / 1000f;

        batteryTotalVolts = batteryAmountSeries * batteryOperatingVolts;
        batteryTotalAmps = batteryAmountParallel * batteryOperatingAmpHours;
        batteryTotalPowerKW = batteryTotalVolts * batteryTotalAmps / 1000f;

        solarStatsText.text = solarTotalVolts.ToString("F2") + " V | " + solarTotalAmps.ToString("F2") + " A | " + solarTotalPowerKW.ToString("0.00") + " kW";

        batteryStatsText.text = batteryTotalVolts.ToString("F2") + " V | " + batteryTotalAmps.ToString("F2") + " Ahr | " + batteryTotalPowerKW.ToString("0.00") + " kWhr";

        float chargingAmps = solarTotalPowerKW*1000f / batteryTotalVolts;
        chargingTimeHrs = batteryTotalAmps/chargingAmps;

        chargingTimeText.text = "Charging Time: " + chargingTimeHrs.ToString("0.0") + " hrs";
    }

    public void UpdateSolarSeriesValue(){
        solarAmountSeries = int.Parse(solarAmountSeriesTxt.text);
        UpdateSolarAmount();
    }

    public void UpdateSolarParallelValue(){
        Debug.Log("parsed solar amount text" + solarAmountParallelTxt.text);
        if(int.Parse(solarAmountParallelTxt.text) <= 0){
            solarAmountParallelTxt.text = "1";
            solarAmountParallel = 1;
        }else{
            solarAmountParallel = int.Parse(solarAmountParallelTxt.text);
        }
        UpdateSolarAmount();
    }

    public void UpdateBatterySeriesValue(){
        batteryAmountSeries = int.Parse(batteryAmountSeriesTxt.text);
        UpdateBatteryAmount();
    }

    public void UpdateBatteryParallelValue(){
        if(int.Parse(batteryAmountParallelTxt.text) <= 0){
            batteryAmountParallelTxt.text = "1";
            batteryAmountParallel = 1;
        }else{
            batteryAmountParallel = int.Parse(batteryAmountParallelTxt.text);
        }
        UpdateBatteryAmount();
    }

    public void UpdateSolarAmount(){
        Debug.Log("update solar amount " + solarAmountSeries + " " + solarAmountParallel);
        for(int i = 0; i < solarContainer.transform.childCount; i++){
            Destroy(solarContainer.transform.GetChild(i).gameObject);
        }

        
        for(int i = 0; i < solarAmountParallel; i++){
            GameObject newRow = Instantiate(solarRowPrefab, solarContainer.transform);
            newRow.GetComponent<RectTransform>().sizeDelta = new Vector2(320f, 50f);
            for(int j = 0; j < solarAmountSeries; j++){
                GameObject newIcon = Instantiate(solarIconPrefab, newRow.transform);
            }
        }
    }

    public void UpdateBatteryAmount(){
        for(int i = 0; i < batteryContainer.transform.childCount; i++){
            Destroy(batteryContainer.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < batteryAmountParallel; i++){
            GameObject newRow = Instantiate(batteryRowPrefab, batteryContainer.transform);
            newRow.GetComponent<RectTransform>().sizeDelta = new Vector2(320f, 40f);
            for(int j = 0; j < batteryAmountSeries; j++){
                GameObject newIcon = Instantiate(batteryIconPrefab, newRow.transform);
            }
        }
    }


}
