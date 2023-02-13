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

    public Text totalEnergyGeneratingText, totalEnergyUsingText, totalEnergyStoringText;

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
