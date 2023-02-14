using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Connector : MonoBehaviour
{
    public EnergyObject energyObject;
    public ObjectInstance objectInstance;

    [System.Serializable]
    public enum ConnectorType{
        Positive,
        Negative,
        ACOut,
        ACIn
    }
    public ConnectorType connectorType;
    public GameObject connectorButton;

    public float currentVolts, currentAmps, currentPower;
    public bool isPositive = false;
    // Start is called before the first frame update
    public void OnInitiate(EnergyObject eo, ObjectInstance oi){

        connectorButton = transform.GetChild(0).gameObject;

        float regularBttnSize = 0.7f;
        float acBttnSize = 0.9f;
        energyObject = eo;
        objectInstance = oi;
        if(connectorType == ConnectorType.Positive || connectorType == ConnectorType.Negative){
            connectorButton.transform.localScale = new Vector3(regularBttnSize, regularBttnSize, regularBttnSize);
        }else if(connectorType == ConnectorType.ACIn || connectorType == ConnectorType.ACOut){
            connectorButton.transform.localScale = new Vector3(acBttnSize, acBttnSize, acBttnSize);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick(){
        Debug.Log("Clicked" + energyObject.name);
    }

    public void SetState(string state){
        //connected, selected, default
        if(state == "connected"){
            connectorButton.GetComponent<Renderer>().material.color = EnergyManager.s.buttonConnectedColor;
        }else if(state == "selected"){
            connectorButton.GetComponent<Renderer>().material.color = EnergyManager.s.buttonSelectedColor;
        }else if(state == "default"){
            connectorButton.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
