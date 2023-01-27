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

    public bool canPlace;

    //all colliders
    public bool useColliders = false;
    public Collider[] colliders;

    public GameObject boundPosition;
    public Vector3 boundOffset;

    //enable object while hovering
    public void OnEnable(){
        canPlace = true;

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

        //initiate colliders
        if(useColliders){
            colliders = GetComponentsInChildren<Collider>();
            InitiateColliders();
        }



        if(GetComponent<EnergyObject>()){
            GetComponent<EnergyObject>().OnInitiateEnergyObject();
        }

        // boundOffset = new Vector3(transform.position.x - boundPosition.transform.position.x, 0, transform.position.z - boundPosition.transform.position.z);


    }

    //when object is clicked and actually placed to the scene
    public void PlaceObject(){
        //Initiate utilities on this object
        if(GetComponent<EnergyGeneratingObject>()){
            GetComponent<EnergyGeneratingObject>().OnPlaceEnergyGeneratingObject();
        }



        objectStatus = "placed";
        SetMaterial("placed");

        if(useColliders){
            foreach(Collider c in colliders){
                c.gameObject.GetComponent<ObjectInstanceCollider>().OnPlaceObject();
          }
        }


        //add yourself to ObjectManager
        ObjectManager.s.objects.Add(this.gameObject);
    }

    //set condition to see if object can be placed
    //if object has already been placed, return
    public void SetPlacingCondition(bool _condition){
        if(objectStatus == "placed"){
            return;
        }

        if(_condition){
            SetMaterial("pending");
        }else{
            SetMaterial("error");
        }
        canPlace = _condition;
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

    void InitiateColliders(){
        foreach(Collider c in colliders){
            c.gameObject.AddComponent<ObjectInstanceCollider>();
            c.gameObject.GetComponent<ObjectInstanceCollider>().OnInitiate(this);
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