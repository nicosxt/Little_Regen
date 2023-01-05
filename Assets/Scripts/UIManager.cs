using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] plantObjectButtons;
    public GameObject currentButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActivePlantButton(string name){
        if(currentButton)
            currentButton.GetComponent<Image>().color = Color.black;
            
        foreach(GameObject obj in plantObjectButtons){
            if(name == obj.name){
                obj.GetComponent<Image>().color = Color.white;
                currentButton = obj;
            }
        }
    }
}
