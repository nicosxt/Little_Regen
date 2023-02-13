using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInfo : MonoBehaviour {

    public GameObject objectPrefab;
    public string objectCategory;
    public string objectName;
    public int objectIndex;
    public Button button;
    public UIButton uiButton;
    public Text text;

    //amount available
    public bool isUnlimited = true;
    public int objectAmountLimit = -1;
    public int objectAmount = 0;

    //UI Button Related
    //default, selected, hover
    public string currentState = "default";

    //how much grid this object takes
    public Vector3 objectSize = new Vector3(1,1,1);
    //Interaction Related
    public bool isSnappable = false;

    public void Start(){
        objectName = gameObject.name.Replace("Object_", "");
        button = GetComponent<Button>();
        button.onClick.AddListener(OnUIButtonClick);
        text = GetComponentInChildren<Text>();
        text.text = objectName;
        
        if(!GetComponent<UIButton>()){
            Debug.Log("No UIButton attached to object " + name);
            return;
        }
        uiButton = GetComponent<UIButton>();

    }

    public void OnUIButtonClick(){
        Debug.Log("OnUIButtonClick " + name);
        ObjectManager.s.currentObjectInfo.SetState("default");
        ObjectManager.s.SetCurrentObjectInfo(this);
        SetState("selected");
    }

    public void SetState(string _s){
        
        if(!uiButton){
            uiButton = GetComponent<UIButton>();
        }

        //Debug.Log("UIButton is " + uiButton);
        currentState = _s;
        if(_s == "default"){
            uiButton.SetState("default");
        } else if(_s == "selected"){
            uiButton.SetState("selected");
        } else if(_s == "hover"){
            uiButton.SetState("hover");
        }
    }

    public bool IsAvailable(){
        if(isUnlimited){
            return true;
        }else{
            if(objectAmount < objectAmountLimit){
                return true;
            }else{
                return false;
            }
        }
    }
}
