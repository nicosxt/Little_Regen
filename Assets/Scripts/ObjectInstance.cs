using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInstance : MonoBehaviour
{

    //pending, placed
    public string objectStatus = "pending";
    public List<Material> originalMaterials = new List<Material>();
    public List<Material> pendingMaterials = new List<Material>();
    public List<Material> errorMaterials = new List<Material>();

    //changing materials
    public MeshRenderer objectMeshRenderer;

    public bool canPlace;

    //all colliders
    public Collider[] colliders;

    public GameObject boundPosition;
    public Vector3 boundOffset;

    //enable object while hovering
    public void OnEnable(){
        canPlace = true;

        objectMeshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();

        //document original materials
        foreach(Material m in objectMeshRenderer.materials){
            Material newMat = new Material(m);
            originalMaterials.Add(newMat);
            pendingMaterials.Add(CategoryManager.s.pendingMaterial);
            errorMaterials.Add(CategoryManager.s.errorMaterial);
        }

        //set material
        SetMaterial("pending");

        name += CategoryManager.s.objectContainer.transform.childCount.ToString();

        //initiate colliders
        colliders = GetComponentsInChildren<Collider>();
        InitiateColliders();

        boundOffset = new Vector3(transform.position.x - boundPosition.transform.position.x, 0, transform.position.z - boundPosition.transform.position.z);


    }

    //when object is clicked and actually placed to the scene
    public void PlaceObject(){
        objectStatus = "placed";
        SetMaterial("placed");
        foreach(Collider c in colliders){
            c.gameObject.GetComponent<ObjectInstanceCollider>().OnObjectPlaced();
        }
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
        objectMeshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();

        if(_status == "pending"){
            objectMeshRenderer.materials = pendingMaterials.ToArray();
        }else if(_status == "placed"){
            objectMeshRenderer.materials = originalMaterials.ToArray();
        }else if(_status == "error"){
            objectMeshRenderer.materials = errorMaterials.ToArray();
        }
    }

    void InitiateColliders(){
        foreach(Collider c in colliders){
            c.gameObject.AddComponent<ObjectInstanceCollider>();
            c.gameObject.GetComponent<ObjectInstanceCollider>().OnInitiate(this);
        }
    }

}