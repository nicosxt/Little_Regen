using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{

    public static EnergyManager s;
    void Awake() {
        if (s != null && s != this) {
            Destroy(this.gameObject);
        } else {
            s = this;
        }
    }

    public float totalEnergy = 1;

    //container prefabs
    public GameObject energyGeneratorContainerPrefab;


    public List<EnergyGeneratingGroup> energyGeneratingGroups = new List<EnergyGeneratingGroup>();

    public List<EnergyGeneratingObject> energyGeneratingObjects = new List<EnergyGeneratingObject>();

    // public List<EnergyStoringObject> EnergyStoringObjects = new List<EnergyStoringObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // public void AddEnergyGeneratingObject(EnergyGeneratingObject obj){
    //     EnergyGeneratingObjects.Add(obj);
    // }

    // Update is called once per frame
    void Update()
    {
        // totalEnergy = 0;
        // foreach(EnergyGeneratingObject obj in EnergyGeneratingObjects){
        //     totalEnergy += obj.currentEnergy;
        // }
    }
}
