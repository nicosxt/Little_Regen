using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager s;
    void Awake() {
        if (s != null && s != this) {
            Destroy(this.gameObject);
        } else {
            s = this;
        }
    }

    //Object Related
    public Material pendingMaterial;
    public Material errorMaterial;

    public List<GameObject> objects = new List<GameObject>();

    public void Initiate(){
        // foreach(CategoryScript cs in CategoryManager.s.categoryScripts){
        //     GameObject newObject = new GameObject();
        //     newObject.name = cs.categoryName;
        //     newObject.transform.parent = transform;
        // }
    }

}
