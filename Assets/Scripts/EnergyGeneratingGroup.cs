using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergyGeneratingGroup : MonoBehaviour
{
    public TextMeshPro indicator;
    public string indicatingString = "Generating: ";

    public List<EnergyGeneratingObject> energyObjects = new List<EnergyGeneratingObject>();

    public float totalEnergy = 0;
    public Vector3 snapPosition;
    public Vector3 positionOffset = new Vector3(2.2f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {


        //updade indicator
        totalEnergy = 0;
        foreach(EnergyGeneratingObject obj in energyObjects){
            totalEnergy += obj.currentEnergy;
        }
        indicator.text = indicatingString + totalEnergy.ToString("0.00") + " kw";
    }

    public void AddToEnergyGroup(EnergyGeneratingObject _obj){
        energyObjects.Add(_obj);
        snapPosition = _obj.transform.position;
        snapPosition += positionOffset;
    }
}
