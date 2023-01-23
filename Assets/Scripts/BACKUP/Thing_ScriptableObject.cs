using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Thing", menuName = "ScriptableObjects/ThingScriptableObject", order = 0)]
public class ThingScriptableObject : ScriptableObject {
    public string thingName;
    public GameObject thingPrefab;

    public string categoryName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
