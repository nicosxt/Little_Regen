using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public float blockSize = 2f;
    float blockObjectSize;
    public float blockAmountSides = 10f;

    public GameObject blockPrefab;
    public GameObject blockParent;
    public List<GameObject> blocks = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        blockObjectSize = blockSize / 2;
        InstantiateBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
