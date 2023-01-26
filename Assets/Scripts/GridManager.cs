using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int amount = 20;
    public float width = 20f;
    public GameObject gridX, gridZ;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateGrids();
        gridX.SetActive(false);
        gridZ.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateGrids(){
        for(int i=1; i<amount; i++){
            Instantiate(gridX, new Vector3(i*width/amount, 0, 0), Quaternion.Euler(0, 180, 0));
            Instantiate(gridZ, new Vector3(0, 0, i*width/amount), Quaternion.Euler(0, -90, 0));
        }
    }
}
