using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInstanceCollider : MonoBehaviour
{
    public ObjectInstance objectInstance;

    public void OnInitiate(ObjectInstance _os){
        objectInstance = _os;
    }

    void OnEnable(){
        if(!GetComponent<Rigidbody>()){
            gameObject.AddComponent<Rigidbody>();
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        GetComponent<Collider>().isTrigger = true;
    }
    
    public void OnPlaceObject(){
        GetComponent<Collider>().isTrigger = false;
    }

    void OnTriggerEnter(Collider other){

        //check if it's colliding with the same object's other colliders

        if(other.gameObject.GetComponent<ObjectInstanceCollider>()){
            if(other.gameObject.GetComponent<ObjectInstanceCollider>().objectInstance == objectInstance){
                return;
            }
        }

        //Debug.Log("entering " + other.name);
        if(other.gameObject.GetComponent<ObjectInstanceCollider>()){
            //do not plant object if colliding with others
            objectInstance.SetPlacingCondition(false);
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.GetComponent<ObjectInstanceCollider>()){
            if(other.gameObject.GetComponent<ObjectInstanceCollider>().objectInstance == objectInstance){
                return;
            }
        }

        objectInstance.SetPlacingCondition(true);
    }
}
