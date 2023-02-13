using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    public Connector connectorFrom, connectorTo;
    public GameObject wireX, wireZ;
    bool doOffsetX = true;//draw L shape or not
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetWire(){
        Vector3 pos1 = connectorFrom.transform.position;
        Vector3 pos2 = connectorTo.transform.position;
        float offsetZ = pos1.z - pos2.z;
        float offsetX = pos2.x - pos1.x;
        
        wireX.transform.localScale = new Vector3(offsetX, 1, 1);
        wireZ.transform.localScale = new Vector3(1, 1, offsetZ);
        if(doOffsetX){
            wireX.transform.localPosition = new Vector3(0, 0, -offsetZ);
            wireZ.transform.localPosition = new Vector3(0, 0, 0);
        }else{
            wireZ.transform.localPosition = new Vector3(offsetX, 0, 0);
            wireX.transform.localPosition = new Vector3(0, 0, 0);
        }
    }
}
