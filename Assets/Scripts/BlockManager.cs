using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlockManager : MonoBehaviour
{
    //Instantiaiting  Blocks
    public float blockSize = 2f;
    float blockObjectSize;
    public float blockAmountSides = 10f;

    public GameObject blockPrefab;
    public GameObject blockParent;
    public List<GameObject> blocks = new List<GameObject>();


    //Click On Blocks
    private InputActions inputActions;
    public Camera camera;

    //Hover On Blocks
    public Material hoverMaterial, defaultMaterial;
    public GameObject hoveredBlock;

    void Awake() {
        inputActions = new InputActions();
    }

    void OnEnable() {
        inputActions.Enable();
    }

    void OnDisable() {
        inputActions.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        blockObjectSize = blockSize / 2;
        InstantiateBlocks();

        inputActions.Default.Click.canceled += ClickOnBlock;
    }

    // Update is called once per frame
    void Update()
    {
        HoverOnBlock();
    }

    void InstantiateBlocks(){
        //Instantiate blocks
        for (int i = 0; i < blockAmountSides; i++)
        {
            for (int j = 0; j < blockAmountSides; j++)
            {
                GameObject newBlock = Instantiate(blockPrefab);
                newBlock.transform.parent = blockParent.transform;
                newBlock.transform.localScale = new Vector3(blockObjectSize, blockObjectSize, blockObjectSize);
                newBlock.transform.localPosition = new Vector3(i * blockSize, 0, j * blockSize);
                newBlock.transform.localRotation = Quaternion.Euler(90f, 0, -90f);
                blocks.Add(newBlock);
            }
        }
        // //Offset middleground
        // blockParent.transform.position = new Vector3(-blockSize * blockAmountSides / 2, 0, -blockSize * blockAmountSides / 2);
    }

    void HoverOnBlock(){
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        Ray ray = new Ray(worldPos, camera.transform.forward);
        RaycastHit hit;
        //Check if it's hitting on the Block Layer  (layer 8)
        if (Physics.Raycast(ray, out hit, 1000f, 1 << 8))
        {
            //if it's hitting on a block
            if(hit.collider.GetComponent<Block>()){
                
                if(hoveredBlock != hit.collider.gameObject){
                    //reset last hoveredBlock
                    if(hoveredBlock)
                        hoveredBlock.GetComponent<Renderer>().material = defaultMaterial;
                        
                    //set new hoveredBlock
                    hoveredBlock = hit.collider.gameObject;
                    hoveredBlock.GetComponent<Renderer>().material = hoverMaterial;
                }
            }
            //Debug.Log("Hover on object: " + hit.collider.gameObject.name);
        }else{
            if(hoveredBlock != null){
                hoveredBlock.GetComponent<Renderer>().material = defaultMaterial;
                hoveredBlock = null;
            }

        }
        
    }

    void ClickOnBlock(InputAction.CallbackContext context){
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        //Debug.Log("Clicking on block");
        
        Ray ray = new Ray(worldPos, camera.transform.forward);
        RaycastHit hit;
        //Check if it's hitting on the Block Layer  (layer 8)
        if (Physics.Raycast(ray, out hit, 1000f, 1 << 8))
        {
            // Debug.Log("Hit object: " + hit.collider.gameObject.name);
            // Debug.Log("Hit point: " + hit.point);
            // Debug.Log("Hit normal: " + hit.normal);

            GameObject hitObject = hit.collider.gameObject;

            if(hitObject.GetComponent<Block>().currentObject == null){
                hitObject.GetComponent<Block>().SpawnObject(ObjectManager.instance.currentObject);


            // ...
            }
        }
    }
}
