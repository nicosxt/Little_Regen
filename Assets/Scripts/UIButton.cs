using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public Button button;
    public Image[] images;
    public Text[] texts;
    
    public void OnInitiate()
    {
        //Debug.Log("Initiate UI Button");
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(string _s){

        if(_s == "default"){
            foreach(Image i in images)
                i.color = Color.black;
            foreach(Text t in texts)
                t.color = Color.black;

        } else if(_s == "selected"){
            foreach(Image i in images)
                i.color = Color.white;
            foreach(Text t in texts)
                t.color = Color.white;

        } else if(_s == "hover"){
            foreach(Image i in images)
                i.color = Color.gray;
            foreach(Text t in texts)
                t.color = Color.gray;
        }
    }
}
