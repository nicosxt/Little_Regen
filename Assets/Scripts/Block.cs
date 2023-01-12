using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public GameObject blockObject;
    public GameObject containedObject;
    public int indexX, indexZ;
    public bool isHighlighted;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initiate(int _x, int _z){
        indexX = _x;
        indexZ = _z;
    }

    //highlightStatus: default, selected, error
    public void SetHighlight(string highlightStatus){
        if(highlightStatus == "default"){
            blockObject.GetComponent<MeshRenderer>().material = BlockManager.s.defaultMaterial;
            isHighlighted = false;
        }else if(highlightStatus == "selected"){
            blockObject.GetComponent<MeshRenderer>().material = BlockManager.s.hoverMaterial;
            isHighlighted = true;
        }else if(highlightStatus == "error"){
            blockObject.GetComponent<MeshRenderer>().material = BlockManager.s.errorMaterial;
            isHighlighted = true;
        }
    }
}
