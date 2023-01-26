using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Manipulator : MonoBehaviour
{
    public static Manipulator s;
    InputActions inputActions;
    void Awake() {
        if (s != null && s != this) {
            Destroy(this.gameObject);
        } else {
            s = this;
        }
        inputActions = new InputActions();
    }
    //public GameObject marker;
    public Camera camera;
    public GameObject measureX, measureZ, textX, textZ, centerX, centerZ;
    public TextMeshPro measureXText, measureZText;
    //public float heightOffset = 0.12f;

    public Vector3 hoverPositionRaw, hoverPositionObject, boundPosition;

    //Object Related
    public bool isHoldingObject = false;

    //Snap related
    public float snapRange = 3f;

    // Start is called before the first frame update
    void Start()
    {
        inputActions.Default.Click.canceled += ClickOnGround;
    }

    void OnEnable() {
        inputActions.Enable();
    }

    void OnDisable() {
        inputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        HoverOnGround();
    }

    void HoverOnGround(){
        //Hovering
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        Ray ray = new Ray(worldPos, camera.transform.forward);
        RaycastHit hit;

        //Check if it's hitting on the Block Layer  (layer 8)
        if (Physics.Raycast(ray, out hit, 1000f, 1 << 8))
        {

            //check with blocks
            BlockManager.s.HoverOnBlocks(hit.collider);

            hoverPositionRaw = hit.point;
            hoverPositionObject = BlockManager.s.GetBlockMedianPosition();
            
            if(!isHoldingObject){
                OnHoverStart(hoverPositionObject);
                isHoldingObject = true;
            }else{
                //offset hoverPosition to currentObjectInstance
                //UpdateMeasurements(hoverPosition);

                //if currentObjectInstance is next to an existing object of the same category, snap to it
                // if(ObjectManager.s.currentObjectScript.isSnappable){
                //     if(currentObjectInstance.GetComponent<EnergyGeneratingObject>().CanSnapToObjects(hoverPositionObject)){
                //         //set currentObjectInstance's position to the snapped position
                //         currentObjectInstance.transform.position = currentObjectInstance.GetComponent<EnergyGeneratingObject>().GetSnappedPosition();
                //     }else{
                //         ObjectManager.s.currentObjectInstance.transform.position = hoverPositionObject;
                //     }
                // }else{
                //     currentObjectInstance.transform.position = hoverPositionObject;
                // }

                ObjectManager.s.currentObjectInstance.transform.position = hoverPositionObject;

                //check if blocks are empty + within boundry
                ObjectManager.s.currentObjectInstance.SetPlacingCondition(BlockManager.s.CheckIfCanPlaceObject());

            }
        }else{
            BlockManager.s.ResetBlocksOnHoverNone();
            OnHoverEnd();
        }
    }

    void OnHoverStart(Vector3 _pos){
        // measureX.SetActive(true);
        // measureZ.SetActive(true);
        // textZ.SetActive(true);
        // textX.SetActive(true);

        //this function also passes value to currentObjectInstance
        CategoryManager.s.currentCategory.PrepareObject(_pos);
    }

    void OnHoverEnd(){
        // measureX.SetActive(false);
        // measureZ.SetActive(false);
        // textZ.SetActive(false);
        // textX.SetActive(false);

        if(isHoldingObject){
            Destroy(ObjectManager.s.currentObjectInstance.gameObject);
            ObjectManager.s.currentObjectInstance = null;
            isHoldingObject = false;
        }
    }

    void UpdateMeasurements(Vector3 _pos){
        boundPosition = _pos - ObjectManager.s.currentObjectInstance.boundOffset;
        measureX.transform.position = new Vector3(_pos.x, 0, 0);
        measureZ.transform.position = new Vector3(0, 0, _pos.z);
        measureZ.transform.localScale = new Vector3(1, 1, boundPosition.x);
        measureX.transform.localScale = new Vector3(1, 1, boundPosition.z);

        textX.transform.position = centerX.transform.position;
        textZ.transform.position = centerZ.transform.position;
        measureXText.SetText(boundPosition.x.ToString("0.00") + "m");
        measureZText.SetText(boundPosition.z.ToString("0.00") + "m");
    }

    void ClickOnGround(InputAction.CallbackContext context){
        if(!isHoldingObject || !ObjectManager.s.currentObjectInstance.GetComponent<ObjectInstance>().canPlace)
            return;

        //Debug.Log("Clicking on block");
        ObjectManager.s.currentObjectInstance.PlaceObject();
        ObjectManager.s.currentObjectInstance = null;
        BlockManager.s.OnPlaceObject();

        //place this object & spawn the next one
        CategoryManager.s.currentCategory.PrepareObject(hoverPositionObject);
        
    }
}
