using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;
    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }


    public List<PlantObject> objects = new List<PlantObject>();
    public GameObject[] objectPrefabs;

    public PlantObject currentObject;
    // Start is called before the first frame update
    void Start()
    {
        //Initiate  all  PlantObjects
        foreach(GameObject obj in objectPrefabs){
            objects.Add(new PlantObject(obj, obj.name));
        }

        currentObject = objects[0];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Change currentObject via UI
    public void SetCurrentObject(string name){
        foreach(PlantObject obj in objects){
            if(obj.name == name){
                currentObject = obj;
            }
        }

        Debug.Log("set curObj to be " + name);
    }
}

public class PlantObject{
    public GameObject gameObject;
    public string name;
    public PlantObject(GameObject obj, string name){
        this.gameObject = obj;
        this.name = name;
    }
}