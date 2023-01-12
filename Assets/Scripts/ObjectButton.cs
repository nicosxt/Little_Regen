using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectButton : MonoBehaviour
{
    // public ObjectScript myObject;
    // public string name;
    // //default, selected, hover
    // public string currentState = "default";

    // public Button button;
    // public Image image;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void Initiate(ObjectScript obj){
    //     button = GetComponent<Button>();
    //     image = GetComponent<Image>();
    //     button.onClick.AddListener(OnClick);
    //     myObject = obj;
    //     gameObject.name = name = obj.name;

    //     //temp labeling
    //     transform.GetChild(0).GetComponent<Text>().text = name;
    // }

    // public void SetState(string _s){
    //     //Debug.Log(name + "setting state " + _s);
    //     currentState = _s;
    //     if(_s == "default"){
    //         image.color = Color.black;
    //         transform.GetChild(0).GetComponent<Text>().color = Color.black;
    //     }else if(_s == "selected"){
    //         image.color = Color.white;
    //         transform.GetChild(0).GetComponent<Text>().color = Color.white;
    //     }
    // }

    // public void OnClick(){
    //     SetState("selected");
    //     //ObjectManager.s.SetCurrentObject(name);
    //     CategoryManager.s.currentCategory.SetCurrentObject(name);
    // }
}
