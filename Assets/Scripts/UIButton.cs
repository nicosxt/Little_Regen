using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    public Button button;
    public Image[] images;
    public Text[] texts;
    
    public void Start()
    {
        //Debug.Log("Initiate UI Button");
        button = GetComponent<Button>();
        images = GetComponentsInChildren<Image>();
        texts = GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(string _s){

        if(_s == "default"){
            SetColor(Color.black);
        } else if(_s == "selected"){
            SetColor(Color.white);
        } else if(_s == "hover"){
            SetColor(Color.gray);
        }
    }

    void SetColor(Color _c){
        foreach(Image i in images)
            i.color = _c;
        foreach(Text t in texts)
            t.color = _c;
        if(GetComponent<Image>()){
            GetComponent<Image>().color = _c;
        }
    }
}
