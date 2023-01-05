using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickToAddd : MonoBehaviour
{
    private InputActions inputActions;
    public Camera camera;
    
    public GameObject plantPrefab;

    public GameObject debugCube;
    

    // Start is called before the first frame update
    void Awake() {
        inputActions = new InputActions();
    }
    

    void OnEnable() {
        inputActions.Enable();
    }

    void OnDisable() {
        inputActions.Disable();
    }

    void Start()
    {
        inputActions.Default.Plant.canceled += OnPlant;
         Debug.DrawRay(new Vector3(0,0,0), camera.transform.forward, Color.green);
    }

    void Update() {
        float buttonPressed = inputActions.Default.Plant.ReadValue<float>();
        if (buttonPressed > 0.5f) {
            Debug.Log("Button pressed");
        }
    }

    void OnPlant(InputAction.CallbackContext context) {
       
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        Ray ray = new Ray(worldPos, camera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, 1 << 8))
        {
            // The raycast hit an object, do something with the hit object
            GameObject hitObject = hit.collider.gameObject;
            Instantiate(plantPrefab, hit.point, Quaternion.identity);

            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            Debug.Log("Hit point: " + hit.point);
            Debug.Log("Hit normal: " + hit.normal);
            // ...
        }
    }
}
