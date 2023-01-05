using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public GameObject pivot;
    public GameObject currentObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject(PlantObject obj){
        currentObject = Instantiate(obj.gameObject, pivot.transform.position, Quaternion.identity);
    }

    public void RemoveObject(){
        Destroy(currentObject);
        currentObject = null;
    }
}
