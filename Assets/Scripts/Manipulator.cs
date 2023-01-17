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
    public GameObject marker;
    public Camera camera;
    public GameObject measureX, measureZ, textX, textZ, centerX, centerZ;
    public TextMeshPro measureXText, measureZText;
    //public float heightOffset = 0.12f;

    public Vector3 hoverPosition, boundPosition;


    //Object Related
    public bool isHoldingObject = false;
    public GameObject currentObject;

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
            //marker.SetActive(true);
            measureX.SetActive(true);
            measureZ.SetActive(true);
            textZ.SetActive(true);
            textX.SetActive(true);
            hoverPosition = hit.point;

            //PrepareObject if haven't
            if(!isHoldingObject){
                //this function also passes value to currentObject
                CategoryManager.s.currentCategory.PrepareObject(hoverPosition);
                isHoldingObject = true;
            }

            if(currentObject != null){
                //offset hoverPosition to currentObject
                boundPosition = hoverPosition - currentObject.GetComponent<ObjectInstance>().boundOffset;

                marker.transform.position = hit.point;
                measureX.transform.position = new Vector3(hit.point.x, 0, 0);
                measureZ.transform.position = new Vector3(0, 0, hit.point.z);
                measureZ.transform.localScale = new Vector3(1, 1, boundPosition.x);
                measureX.transform.localScale = new Vector3(1, 1, boundPosition.z);

                textX.transform.position = centerX.transform.position;
                textZ.transform.position = centerZ.transform.position;
                measureXText.SetText(boundPosition.x.ToString("0.00") + "m");
                measureZText.SetText(boundPosition.z.ToString("0.00") + "m");

                currentObject.transform.position = hoverPosition;
            }


        }else{
            //marker.SetActive(false);
            measureX.SetActive(false);
            measureZ.SetActive(false);
            textZ.SetActive(false);
            textX.SetActive(false);

            if(isHoldingObject){
                Destroy(currentObject);
                isHoldingObject = false;
            }
        }
    }

    void ClickOnGround(InputAction.CallbackContext context){
        if(!isHoldingObject || !currentObject.GetComponent<ObjectInstance>().canPlace)
            return;

        //Debug.Log("Clicking on block");
        currentObject.GetComponent<ObjectInstance>().PlaceObject();
        currentObject = null;
        //place this object & spawn the next one
        CategoryManager.s.currentCategory.PrepareObject(hoverPosition);
        
    }
}
