using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGeneratingObject : EnergyObject
{
    public float totalCapacity = 1;//kw/dad/panel
    public float chargingRate = 1;//amount of energy able to receive per hour
    public float currentEnergy = 0;

    //if is close to another EnergyObject, snap to the object
    public EnergyGeneratingObject snapTarget;
    public EnergyGeneratingGroup energyGroup;
    
    public void OnPlaceObject(){

        if(!snapTarget){
            //if this is the first one in its group, spawn the energyGroup
            GameObject container = Instantiate(EnergyManager.s.energyGeneratorContainerPrefab, transform.position, Quaternion.identity);
            container.transform.parent = ObjectManager.s.transform;

            // initiating energyGroup
            energyGroup = container.GetComponent<EnergyGeneratingGroup>();
            //add energy group to EnergyManager
            EnergyManager.s.energyGeneratingGroups.Add(energyGroup);

        }else{
            //assigning energyGroup
            energyGroup = snapTarget.energyGroup;
        }

        transform.parent = energyGroup.transform;

        //add to energyGroup and EnergyManager
        energyGroup.AddToEnergyGroup(this);
        EnergyManager.s.energyGeneratingObjects.Add(this);
    }

    public Vector3 GetSnappedPosition(){
        return snapTarget.energyGroup.snapPosition;
    }

    public bool CanSnapToObjects(Vector3 _hoverPosition){
        if(EnergyManager.s.energyGeneratingGroups.Count < 0)
            return false;
        
        bool canSnap = false;

        foreach(EnergyGeneratingObject obj in EnergyManager.s.energyGeneratingObjects){
            //check proximity
            float range = Vector3.Distance(_hoverPosition, obj.transform.position);
            
            if(range < Manipulator.s.snapRange){
                //this can be snapped to another object
                if(snapTarget == null)
                    snapTarget = obj;

                canSnap = true;
            }
        }

        if(!canSnap)
            snapTarget = null;

        return (snapTarget == null) ? false : true;
    }

    void Update(){
        currentEnergy = totalCapacity * chargingRate;
    }

    //if is close to another EnergyObject, snap to the object
}
