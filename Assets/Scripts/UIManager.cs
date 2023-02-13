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

    public GameObject menuContainer, modeButtonContainer;
    public string currentMode = "Place";

    public Text totalEnergyGeneratingText, totalEnergyUsingText, totalEnergyStoringText;

    // Start is called before the first frame update
    void Start()
    {

        SetCurrentMode(currentMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentMode(string _name){
        // currentMode = _name;
        // foreach(Transform t in menuContainer.transform){
        //     t.gameObject.SetActive(t.name == _name);
        // }
        // foreach(Transform t in modeButtonContainer.transform){
        //     t.GetComponent<UIButton>().SetState(t.name == _name ? "selected" : "default");
        // }
        //foreach()
    }

}
