using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public ObjectInstance objectInstance;
    public Renderer skin;
    //all available animations
    public Animator myAnimator;
    public int animationIndex, colorIndex;
    string[] animations = {"Ramba", "Hiphop", "YMCA", "Silly"};


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable(){
        animationIndex = Random.Range(0, animations.Length);
        myAnimator.Play("Main." + animations[animationIndex]);
    }

    public void OnInitiate(ObjectInstance o){
        objectInstance = o;

        PeopleManager.s.allPeople.Add(this);

        //set color from PeopleManager's allColors array
        colorIndex = Random.Range(0, PeopleManager.s.allColors.Length);
        Color myColor = PeopleManager.s.allColors[colorIndex];
        skin.material.color = myColor;
    }
}
