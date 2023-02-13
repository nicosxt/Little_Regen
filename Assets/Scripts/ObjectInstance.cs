using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectInstance : MonoBehaviour
{

    //pending, placed
    public string objectStatus = "pending";

    //changing materials
    public MeshRenderer[] objectMeshRenderers;

    public List<RendererMaterialPair> rendererMaterialPairs = new List<RendererMaterialPair>();

    //this variable can be affected by utility scripts attached to the object
    public bool hasPlacingConstraints = false;

    //if true, every object can only be spawned next to the first object
    public EnergyObject energyObject;
    public bool hasOneGroup = false;
    public bool isFirstInGroup = false;

    //temporary obsolete!
    public GameObject boundPosition;
    public Vector3 boundOffset;

    //enable object while hovering
    public void OnEnable(){

        //Generator and Batteries would result in hasOneGroup to be true

        if(GetComponent<EnergyObject>()){
            energyObject = GetComponent<EnergyObject>();
            energyObject.OnEnable();

            //conditions for objects that needs to be grouped [PROBABLY DEPRECATED]
            // if(GetComponent<Battery>()){
            //     hasOneGroup = true;
            //     isFirstInGroup = (EnergyManager.s.batteries.Count == 0);
            // }else if(GetComponent<Generator>()){
            //     hasOneGroup = true;
            //     isFirstInGroup = (EnergyManager.s.generators.Count == 0);
            // }
        }



        objectMeshRenderers = transform.GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer objectMeshRenderer in objectMeshRenderers){
            if(objectMeshRenderer.GetComponent<TextMeshPro>()){
                //remove objectMeshRenderer if it is TextMeshPro
                List<MeshRenderer> temp = new List<MeshRenderer>(objectMeshRenderers);
                temp.Remove(objectMeshRenderer);
                objectMeshRenderers = temp.ToArray();
            }
        }

        //document original materials
        foreach(MeshRenderer objectMeshRenderer in objectMeshRenderers){
            rendererMaterialPairs.Add(new RendererMaterialPair(objectMeshRenderer));
            
        }

        //set material
        SetMaterial("pending");

        name += ObjectManager.s.transform.childCount.ToString();


    }

    void Update(){
    }


    //when object is clicked and actually placed to the scene
    public void PlaceObject(){

        objectStatus = "placed";
        SetMaterial("placed");

        if(energyObject)
            energyObject.OnInitiate(this);

        //add yourself to ObjectManager
        ObjectManager.s.objectInstances.Add(this.gameObject);
        //increase object count for this type
        ObjectManager.s.currentObjectInfo.objectAmount ++;
    }

    //set condition to see if object can be placed via Block logic
    //if object has already been placed, return
    public void SetPlacingCondition(bool _canPlace){
        if(objectStatus == "placed"){
            return;
        }

        if(_canPlace){
            SetMaterial("pending");
        }else{
            SetMaterial("error");
        }
    }


    //set material 
    public void SetMaterial(string _status){

        foreach(RendererMaterialPair rmPair in rendererMaterialPairs){

            if(_status == "pending"){
                rmPair.renderer.materials = rmPair.pendingMaterials;
            }else if(_status == "placed"){
                rmPair.renderer.materials = rmPair.materials;
            }else if(_status == "error"){
                rmPair.renderer.materials = rmPair.errorMaterials;
            }
        }
    }

    public bool CanPlaceObject(){
        //check if is Next To Previous Grouped Object
        if(energyObject && hasOneGroup && !isFirstInGroup){
            if(energyObject is Battery){
                float xDiff = Mathf.Abs(transform.position.x - EnergyManager.s.batteries[EnergyManager.s.batteries.Count - 1].transform.position.x);
                float zDiff = Mathf.Abs(transform.position.z - EnergyManager.s.batteries[EnergyManager.s.batteries.Count - 1].transform.position.z);
                if(xDiff <= BlockManager.s.blockSize && zDiff <= BlockManager.s.blockSize){
                    //is next to the previous block
                    return true;
                }else{
                    return false;
                }
            }else if(energyObject is Generator){
                float xDiff = Mathf.Abs(transform.position.x - EnergyManager.s.generators[EnergyManager.s.generators.Count - 1].transform.position.x);
                float zDiff = Mathf.Abs(transform.position.z - EnergyManager.s.generators[EnergyManager.s.generators.Count - 1].transform.position.z);
                if(xDiff <= BlockManager.s.blockSize && zDiff <= BlockManager.s.blockSize){
                    //is next to the previous block
                    return true;
                }else{
                    return false;
                }
            }else{
                return true;
            }
        }else{
            return true;
        }
    }


}

[Serializable]
public class RendererMaterialPair{
    public MeshRenderer renderer;
    public Material[] materials;
    public Material[] pendingMaterials;
    public Material[] errorMaterials;

    public RendererMaterialPair(MeshRenderer _renderer){

        List<Material> pendingMaterialsList = new List<Material>();
        List<Material> errorMaterialsList = new List<Material>();
        
        this.renderer = _renderer;
        this.materials = _renderer.materials;

        foreach(Material m in this.materials){
            Material newPendingMat = new Material(ObjectManager.s.pendingMaterial);
            Material newErrorMat = new Material(ObjectManager.s.errorMaterial);
            pendingMaterialsList.Add(newPendingMat);
            errorMaterialsList.Add(newErrorMat);
        }

        this.pendingMaterials = pendingMaterialsList.ToArray();
        this.errorMaterials = errorMaterialsList.ToArray();

    }
}