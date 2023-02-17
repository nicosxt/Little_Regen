using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager s;
    void Awake() {
        if (s != null && s != this) {
            Destroy(this.gameObject);
        } else {
            s = this;
        }
    }

    public GameObject objectButtonPrefab;
    public GameObject objectButtonContainer;

    public  GameObject categoryButtonPrefab;
    public GameObject categoryButtonContainer;

    public GameObject sidebarContainer, menuContainer, modeButtonContainer;

    public Text solarAmount, solarStats, batteryAmount, batteryStats, applianceAmount, applianceStats;

    public Button useModeButton;

    // Start is called before the first frame update
    void Start()
    {
        SetCurrentMode("design");
        useModeButton.onClick.AddListener(ToggleUseMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentMode(string _name){
        Manipulator.s.currentMode = _name;
        sidebarContainer.SetActive(_name == "design");

        //toggle connectors on all EnergyObjects 
        // foreach(EnergyObject energyObject in EnergyManager.s.energyObjects){
        //     energyObject.ToggleConnectors(_name == "use");
        // }

    }

    public void UpdateSolarStats(int _amount, float _V, float _A, float _W){
        solarAmount.text = "x" + _amount.ToString();
        //display status at different lines for solarStats
        solarStats.text = _V.ToString("0.0") + "V" + "\n" + _A.ToString("0.0") + "A" + "\n" + (_W/1000f).ToString("0.0") + "kW";
    }

    public void UpdateBatteryStats(int _amount, float _totalW, float _currentW, float _percentage, float _V, float _inAmps, float _outAmps){
        batteryAmount.text = "x" + _amount.ToString();
        //display status at different lines for batteryStats
        batteryStats.text = (_currentW/1000f).ToString("0.0") + "kW/" + (_totalW/1000f).ToString("0.0") + "kW [" + _percentage.ToString("0") + "%]" + "\n" + _V.ToString("0.0") + "V" + "\n" + "In: " + _inAmps.ToString("0.0") + "A" + "\n" + "Out:" + _outAmps.ToString("0.0") + "A";

    }

    public void UpdateAppliancesStats(int _amount, float _totalA, float _totalV){
        applianceAmount.text = "x" + _amount.ToString();
        //display status at different lines for applianceStats
        applianceStats.text = _totalV.ToString("0") + "V" + "\n" + _totalA.ToString("0.0") + "A";
    }

    public void ToggleUseMode(){
        if(Manipulator.s.currentMode == "use"){
            SetCurrentMode("design");
            useModeButton.GetComponent<UIButton>().SetState("default");
        } else if(Manipulator.s.currentMode == "design"){
            SetCurrentMode("use");
            useModeButton.GetComponent<UIButton>().SetState("selected");
        }
    }

}
