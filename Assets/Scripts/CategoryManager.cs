using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryManager : MonoBehaviour
{   
    [Header("Database for all categoryScripts in game")]
    [Space(35)]
    public CategoryScript currentCategory;
    public List<CategoryScript> categoryScripts = new List<CategoryScript>();

    public static CategoryManager s;
    void Awake() {
        if (s != null && s != this) {
            Destroy(this.gameObject);
        } else {
            s = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        //populate categoryScripts
        int count = 0;
        foreach(Transform child in transform){
            categoryScripts.Add(child.GetComponent<CategoryScript>());
            child.GetComponent<CategoryScript>().Initiate(count);
            count ++;
        }

        SetCurrentCategory(categoryScripts[0].categoryName);

        //initiate object scripts
        ObjectManager.s.Initiate();
    }

    // public void GenerateCategoriesInObjectContainer(){
    //     foreach(Transform tr in transform){
    //         GameObject newObject = new GameObject();
    //         newObject.name = tr.name;
    //         newObject.transform.parent = objectContainer.transform;

    //     }
    // }

    public void SetCurrentCategory(string name){
        foreach(CategoryScript ctg in categoryScripts){
            if(ctg.categoryName == name){
                ctg.SetState("selected");
                currentCategory = ctg;
            }else{
                ctg.SetState("default");
            }
        }

        
    }
}
