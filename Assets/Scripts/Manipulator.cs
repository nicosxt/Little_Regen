using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

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
    
    //2d to 3d switch
    public Button perspectiveSwitchButton;
    public Vector3 cameraPos3D, cameraPos2D, cameraRot3D, cameraRot2D;
    public float cameraSize3D, cameraSize2D;
    public bool isCamera2D;


    //design, use
    public string currentMode = "design";

    
    //measurements
    // public GameObject measureX, measureZ, textX, textZ, centerX, centerZ;
    // public TextMeshPro measureXText, measureZText;
    //public float heightOffset = 0.12f;

    public Vector3 hoverPositionOnBlocks, boundPosition;

    //Object Related
    public bool isHoldingObject = false;

    //Snap related
    // public float snapRange = 3f;

    //Hover related
    public GameObject hoveringObject;
    public Vector3 hoveringPosition;
    Vector2 mousePos;
    Vector3 worldPos;
    //Globally accessible Ray (for Mouse action for now)
    public Ray mouseRay;

    //Placing Conditiopn
    public bool canPlace = false;

    //Energy Related

    // Start is called before the first frame update
    void Start()
    {
        perspectiveSwitchButton.onClick.AddListener(ToggleCamera);
        inputActions.Default.Click.canceled += OnClick;
        cameraPos3D = camera.transform.position;
        cameraRot3D = camera.transform.eulerAngles;
        cameraPos2D = new Vector3(12.2f, 22.7f, 8.1f);
        cameraRot2D = new Vector3(90f, 0f, 0f);
        cameraSize2D = 16.1f;
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

        //Hovering
        mousePos = Mouse.current.position.ReadValue();
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        //Update Ray for everybody
        mouseRay = new Ray(worldPos, camera.transform.forward);

        if(currentMode == "design"){
            HoverOnGround();
        }else if(currentMode == "use"){
            HoverOnObjects();
        }
        
    }

    public void ToggleCamera(){
        perspectiveSwitchButton.GetComponentInChildren<TextMeshProUGUI>().text = isCamera2D ? "3D" : "2D";
        if(isCamera2D){
            LerpCamera(cameraPos3D, cameraRot3D);
        }else{
            LerpCamera(cameraPos2D, cameraRot2D);
        }
        camera.orthographicSize = isCamera2D ? cameraSize3D : cameraSize2D;
        isCamera2D = !isCamera2D;
    }

    void LerpCamera(Vector3 _pos, Vector3 _rot){
        camera.transform.position = _pos;
        camera.transform.eulerAngles = _rot;
    }

    void HoverOnObjects(){
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit, 1000f, 1 << 7))
        {
            hoveringObject = hit.collider.gameObject;
            //Debug.Log(hit.collider.name);

            if(hit.collider.GetComponent<EnergyObject>()){
                hoveringObject = hit.collider.gameObject;
            }else if (hit.collider.GetComponentInParent<EnergyObject>()){
                hoveringObject = hit.collider.transform.parent.gameObject;
            }
        }else{
            hoveringObject = null;
        }

    }

    void HoverOnGround(){

        if(!ObjectManager.s.currentObjectInfo.IsAvailable()){
            isHoldingObject = false;
            return;
        }

        RaycastHit hit;
        //Check if it's hitting on the Block Layer  (layer 8)
        if (Physics.Raycast(mouseRay, out hit, 1000f, 1 << 8))
        {

            //check with blocks
            BlockManager.s.HoverOnBlocks(hit.collider);

            hoveringPosition = hit.point;
            hoverPositionOnBlocks = BlockManager.s.GetBlockMedianPosition();
            
            if(!isHoldingObject){
                OnHoverStart(hoverPositionOnBlocks);
                isHoldingObject = true;
            }else{
                ObjectManager.s.currentObjectInstance.transform.position = hoverPositionOnBlocks;

                //check if blocks are empty + within boundry
                //and check if object doesn't have placing constraints
                canPlace = BlockManager.s.CheckIfCanPlaceObject() && 
                ObjectManager.s.currentObjectInstance.CanPlaceObject();

                BlockManager.s.SetBlockPlacingCondition(canPlace);
                ObjectManager.s.currentObjectInstance.SetPlacingCondition(canPlace);

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
        ObjectManager.s.PrepareObject(_pos);
    }

    void OnHoverEnd(){
        // measureX.SetActive(false);
        // measureZ.SetActive(false);
        // textZ.SetActive(false);
        // textX.SetActive(false);

        if(isHoldingObject && ObjectManager.s.currentObjectInstance){
            Destroy(ObjectManager.s.currentObjectInstance.gameObject);
            ObjectManager.s.currentObjectInstance = null;
            isHoldingObject = false;
        }
    }

    void UpdateMeasurements(Vector3 _pos){
        // boundPosition = _pos - ObjectManager.s.currentObjectInstance.boundOffset;
        // measureX.transform.position = new Vector3(_pos.x, 0, 0);
        // measureZ.transform.position = new Vector3(0, 0, _pos.z);
        // measureZ.transform.localScale = new Vector3(1, 1, boundPosition.x);
        // measureX.transform.localScale = new Vector3(1, 1, boundPosition.z);

        // textX.transform.position = centerX.transform.position;
        // textZ.transform.position = centerZ.transform.position;
        // measureXText.SetText(boundPosition.x.ToString("0.00") + "m");
        // measureZText.SetText(boundPosition.z.ToString("0.00") + "m");
    }

    void OnClick(InputAction.CallbackContext context){

        //Debug.Log("Clicking manipulator");
        //If hovering on Object
        if(hoveringObject){
            hoveringObject.GetComponent<EnergyObject>().OnClick();
            Debug.Log("Click");
        }

        if(currentMode == "use"){
            EnergyManager.s.OnClick();
        }
        
        //Clickong on Ground
        if(!isHoldingObject || !canPlace)
            return;

        //Debug.Log("Clicking on block");

        if(ObjectManager.s.currentObjectInfo.IsAvailable()){
            ObjectManager.s.currentObjectInstance.PlaceObject();
            ObjectManager.s.currentObjectInstance = null;
            BlockManager.s.OnPlaceObject();
            //place this object & spawn the next one
            ObjectManager.s.PrepareObject(hoverPositionOnBlocks);
        }else{
            ObjectManager.s.currentObjectInstance = null;
        }



        
    }
}
