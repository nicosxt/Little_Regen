using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public GameObject measureX, measureZ;
    //public float heightOffset = 0.12f;

    public Vector3 hoverPosition;

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
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        Ray ray = new Ray(worldPos, camera.transform.forward);
        RaycastHit hit;
        //Check if it's hitting on the Block Layer  (layer 8)
        if (Physics.Raycast(ray, out hit, 1000f, 1 << 8))
        {
            marker.SetActive(true);
            measureX.SetActive(true);
            measureZ.SetActive(true);
            hoverPosition = hit.point;
            marker.transform.position = hit.point;
            measureX.transform.position = new Vector3(hit.point.x, 0, 0);
            measureZ.transform.position = new Vector3(0, 0, hit.point.z);
            measureZ.transform.localScale = new Vector3(1, 1, hit.point.x);
            measureX.transform.localScale = new Vector3(1, 1, hit.point.z);
        }else{
            hoverPosition = new Vector3(-1,-1,-1);
            marker.SetActive(false);
            measureX.SetActive(false);
            measureZ.SetActive(false);
        }
    }

    void ClickOnGround(InputAction.CallbackContext context){
        if(hoverPosition == new Vector3(-1,-1,-1))
            return;
        //Debug.Log("Clicking on block");
        CategoryManager.s.currentCategory.SpawnObject(hoverPosition);
    }
}
