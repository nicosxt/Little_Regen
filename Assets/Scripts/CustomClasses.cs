using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object{
    public GameObject gameObject;
    public string name;
    public ObjectButton button;

    public Vector3 objectSize;
    public Object(GameObject obj, string name, ObjectButton button, Vector3 size){
        this.gameObject = obj;
        this.name = name;
        this.button = button;
        this.objectSize = size;

        //initiate on the object script
    }
}