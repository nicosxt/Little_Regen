using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //Organizational
    public RectTransform objectInfoContainer;
    public List<ObjectInfo> objectInfos = new List<ObjectInfo>();

    //Object Related
    public Material pendingMaterial;
    public Material errorMaterial;

    public List<GameObject> objectInstances = new List<GameObject>();

    //Current
    public ObjectInfo currentObjectInfo;
    public ObjectInstance currentObjectInstance;
    
    //Debug
    public GameObject debugObject;

    public void Start(){
        //Initiate Object Info

        foreach(RectTransform t in objectInfoContainer){
            if(t.GetComponent<ObjectInfo>()){
                ObjectInfo o = t.GetComponent<ObjectInfo>();
                objectInfos.Add(o);
            }
        }
        //Debug.Log("Object Info Count " + objectInfos.Count);
        for(int i=0; i<objectInfos.Count; i++){
            objectInfos[i].SetState((i==0) ? "selected" : "default");
        }
        SetCurrentObjectInfo(objectInfos[0]);
        //Debug.Log(currentObjectInfo.objectName);
    }

    public void SetCurrentObjectInfo(ObjectInfo _o){
        //Debug.Log("set current object info " + _o.objectName);
        currentObjectInfo = _o;
    }

    public void PrepareObject(Vector3 _pos){
        if(currentObjectInfo.objectAmountLimit != -1 && currentObjectInfo.objectAmount >= currentObjectInfo.objectAmountLimit){
            Debug.Log("Object amount limit reached");
            return;
        }
        GameObject obj = Instantiate(currentObjectInfo.objectPrefab, _pos, Quaternion.identity, transform);
        currentObjectInstance = obj.GetComponent<ObjectInstance>();
    }

}
