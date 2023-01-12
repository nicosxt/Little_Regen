using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlockManager : MonoBehaviour
{

    [Header("Database for all Blocks in game + Instantiate Blocks on Start")]
    [Space(35)]

    //Instantiaiting  Blocks
    public float blockSize = 2f;
    float blockObjectSize;
    public int blockAmountX = 10;
    public int blockAmountZ = 10;
    public float blockOffsetX, blockOffsetZ;
    public float landWidth;

    public GameObject blockParent;
    public GameObject blockPrefab;
    public List<Block> blocks = new List<Block>();

    //Hover On Blocks
    public Material hoverMaterial, defaultMaterial, errorMaterial;
    public Block hoveredBlock;
    public Vector2 hoveredBlockRangeX, hoveredBlockRangeZ;
    public Vector3 blockMedianPosition;
    public bool isHoverWithinBoundry;
    public List<Block> hoveredBlocks = new List<Block>();

    //Click On Blocks
    private InputActions inputActions;
    public Camera camera;


    public static BlockManager s;
    void Awake() {
        if (s != null && s != this) {
            Destroy(this.gameObject);
        } else {
            s = this;
        }
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
        blockOffsetX = blockSize * blockAmountX/2;
        blockOffsetZ = blockSize * blockAmountZ/2;
        float blockSideX = blockSize * blockAmountX;
        float blockSideZ = blockSize * blockAmountZ;
        landWidth = Mathf.Sqrt(blockSideX * blockSideX + blockSideZ * blockSideZ);

        InstantiateBlocks();
        camera.orthographicSize = landWidth * 0.36f;
        inputActions.Default.Click.canceled += ClickOnBlock;
    }

    // Update is called once per frame
    void Update()
    {
        HoverOnBlock();
    }

    void InstantiateBlocks(){
        //Instantiate blocks
        for (int i = 0; i < blockAmountX; i++)
        {
            for (int j = 0; j < blockAmountZ; j++)
            {
                GameObject newBlock = Instantiate(blockPrefab);
                newBlock.transform.parent = blockParent.transform;
                newBlock.transform.localScale = new Vector3(blockSize, blockSize, blockSize);
                
                newBlock.transform.localPosition = new Vector3(i * blockSize - blockOffsetX + blockSize/2, 0, j * blockSize - blockOffsetZ+ blockSize/2);
                newBlock.transform.localRotation = Quaternion.Euler(0, 0, 0);

                newBlock.GetComponent<Block>().Initiate(i, j);
                blocks.Add(newBlock.GetComponent<Block>());
            }
        }
    }

    void HoverOnBlock(){
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        Ray ray = new Ray(worldPos, camera.transform.forward);
        RaycastHit hit;
        //Check if it's hitting on the Block Layer  (layer 8)
        if (Physics.Raycast(ray, out hit, 1000f, 1 << 8))
        {
            //if it's hitting on a block, get index of the block
            if(hit.collider.transform.parent.GetComponent<Block>()){

                ObjectScript currentObjectScript = CategoryManager.s.currentCategory.currentObjectScript;

                hoveredBlock = hit.collider.transform.parent.GetComponent<Block>();
                hoveredBlockRangeX = new Vector2(hoveredBlock.indexX, hoveredBlock.indexX + currentObjectScript.objectSize.x - 1);
                hoveredBlockRangeZ = new Vector2(hoveredBlock.indexZ, hoveredBlock.indexZ - currentObjectScript.objectSize.z + 1);

                if(hoveredBlockRangeX.y >= blockAmountX || hoveredBlockRangeZ.y < 0){
                    isHoverWithinBoundry = false;
                }else{
                    isHoverWithinBoundry = true;
                }

                //Check who is in hoveredBlocks
                foreach (Block block in blocks)
                {
                    if(CheckIfBlockIsWithinIndexRange(block, hoveredBlockRangeX, hoveredBlockRangeZ)){
                        if(!hoveredBlocks.Contains(block)){
                            hoveredBlocks.Add(block);
                        }
                    }else{
                        //block.SetHighlight("none");
                        block.SetHighlight("default");
                        if(hoveredBlocks.Contains(block)){
                            hoveredBlocks.Remove(block);
                            
                        }
                    }
                }
                

                foreach (Block block in hoveredBlocks)
                {
                    block.SetHighlight((CheckIfBlocksAreEmpty(hoveredBlocks) && isHoverWithinBoundry) ? "selected" : "error");
                }


            }else{
                ResetBlocksOnHoverNone();
            }
        }else{
            ResetBlocksOnHoverNone();
        }
        
    }

    void ResetBlocksOnHoverNone(){
        hoveredBlock = null;
        hoveredBlocks.Clear();
        hoveredBlockRangeX = hoveredBlockRangeZ = Vector2.zero;

        foreach (Block block in blocks)
        {
            block.SetHighlight("default");
        }
    }

    bool CheckIfBlockIsWithinIndexRange(Block _b, Vector2 _x, Vector2 _z){
        if(_b.indexX >= _x.x && _b.indexX <= _x.y && _b.indexZ <= _z.x && _b.indexZ >= _z.y){
            return true;
        }
        return false;
    }

    bool CheckIfBlocksAreEmpty(List<Block> _blocks){
        foreach(Block block in _blocks){
            if(block.containedObject){
                return false;
            }
        }
        return true;
    }

    void ClickOnBlock(InputAction.CallbackContext context){
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));

        //Debug.Log("Clicking on block");

        if(hoveredBlocks.Count > 0 && CheckIfBlocksAreEmpty(hoveredBlocks) && isHoverWithinBoundry){

            //check if all blocks are empty
            blockMedianPosition = Vector3.zero;
            ObjectScript currentObjectScript = CategoryManager.s.currentCategory.currentObjectScript;
            foreach(Block block in hoveredBlocks){
                block.containedObject = currentObjectScript.gameObject;
                blockMedianPosition += block.transform.position;
            }
            blockMedianPosition /= hoveredBlocks.Count;
            CategoryManager.s.currentCategory.SpawnObject(blockMedianPosition);
        }
    }
}
