using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectScript : MonoBehaviour {
    //about object category
    public CategoryScript objectCategory;

    //about this object
    public GameObject objectPrefab;
    public string objectName;
    public int objectIndex;
    //how much grid this object takes
    public Vector3 objectSize = new Vector3(1,1,1);

    //UI Button Related
    //default, selected, hover
    public string currentState = "default";
    public GameObject UIButton;
    public Button button;
    public Image image;
    public Text text;

    public void Initiate(int _i, CategoryScript _ctg){
        objectCategory = _ctg;
        
        objectName = gameObject.name;
        objectIndex = _i;

        UIButton = Instantiate(UIManager.s.objectButtonPrefab, UIManager.s.objectButtonContainer.transform);
        button = UIButton.GetComponent<Button>();
        image = UIButton.GetComponent<Image>();
        text = UIButton.transform.GetChild(0).GetComponent<Text>();
        button.onClick.AddListener(OnUIButtonClick);
        text.text = objectName;
    }

    public void OnUIButtonClick(){
        CategoryManager.s.currentCategory.SetCurrentObject(name);
    }

    public void SetState(string _s){
        //Debug.Log(name + "setting state " + _s);
        currentState = _s;
        if(_s == "default"){
            image.color = Color.black;
            UIButton.transform.GetChild(0).GetComponent<Text>().color = Color.black;
        }else if(_s == "selected"){
            image.color = Color.white;
            UIButton.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        }
    }
}
