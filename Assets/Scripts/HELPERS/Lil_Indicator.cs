using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lil_Indicator : MonoBehaviour
{
    public GameObject amountTextContainer;
    public TextMeshPro amountText;
    
    public bool showShape;
    public GameObject amountPercentageContainer;
    public Transform amountPercentage;
    public string amountTextFormat = "0.0";
    public string amountUnit = "kw";
    // Start is called before the first frame update
    void Start()
    {
        amountPercentageContainer.SetActive(showShape);
        amountTextContainer.transform.localPosition = new Vector3(showShape ? 0.8f : 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateAmount(float amount, float totalAmount = 0){
        amountText.text = amount.ToString(amountTextFormat) + " " + amountUnit;
        if(showShape)
            amountPercentage.localScale = new Vector3(amount/totalAmount, 1, 1);
    }
}
