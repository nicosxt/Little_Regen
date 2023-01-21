using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryScript : MonoBehaviour
{
    public int categoryIndex;
    public string categoryName;

    //UI Button Related
    //default, selected, hover
    public string currentState = "default";
    public GameObject UIButton;
    public Button button;
    public Image image;
    public Text text;

    //Objects of this category
    public List<ObjectScript> objectScripts = new List<ObjectScript>();
    public ObjectScript currentObjectScript;

    public void Initiate(int _i){
        categoryIndex = _i;
        categoryName = gameObject.name;
        
        //initiate UI button
        UIButton = Instantiate(UIManager.s.categoryButtonPrefab, UIManager.s.categoryButtonContainer.transform);
        button = UIButton.GetComponent<Button>();
        image = UIButton.GetComponent<Image>();
        text = UIButton.transform.GetChild(0).GetComponent<Text>();
        button.onClick.AddListener(OnUIButtonClick);
        text.text = categoryName;

        //initiate objects
        int count = 0;
        foreach(Transform child in transform){
            objectScripts.Add(child.GetComponent<ObjectScript>());
            child.GetComponent<ObjectScript>().Initiate(count, this);
            count ++;
        }

    }

    public void OnUIButtonClick(){
        CategoryManager.s.SetCurrentCategory(name);
    }


    public void SetState(string _s){
        //Debug.Log(name + "setting state " + _s);
        currentState = _s;
        if(_s == "default"){
            image.color = Color.black;
            UIButton.transform.GetChild(0).GetComponent<Text>().color = Color.black;
            
            //disable all buttons in this category
            foreach(ObjectScript obj in objectScripts){
                obj.UIButton.SetActive(false);
            }
            
        }else if(_s == "selected"){
            image.color = Color.white;
            UIButton.transform.GetChild(0).GetComponent<Text>().color = Color.white;
            
            //enable all buttons in this category
            foreach(ObjectScript obj in objectScripts){
                obj.UIButton.SetActive(true);
            }

            //set a default object
            if(objectScripts.Count > 0)
                SetCurrentObject(objectScripts[0].objectName);
        }
    }

    //Change currentObjectScript via UI
    public void SetCurrentObject(string name){
        foreach(ObjectScript obj in objectScripts){
            if(obj.name == name){
                obj.SetState("selected");
                currentObjectScript = obj;
            }else{
                obj.SetState("default");
            }
        }
    }

    public void PrepareObject(Vector3 _pos){
        //Quaternion _rot = Quaternion.identity;
        // _rot.eulerAngles = new Vector3(0, 45f, 0);
        GameObject newObject = Instantiate(currentObjectScript.objectPrefab, _pos, Quaternion.identity, ObjectManager.s.transform);

        Manipulator.s.currentObject = newObject;
    }


}
