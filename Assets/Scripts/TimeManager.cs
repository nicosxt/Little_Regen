using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager s;
    void Awake() {
        if (s != null && s != this) {
            Destroy(this.gameObject);
        } else {
            s = this;
        }
    }

    //1f is 1 minute
    //60f is 1 hour
    public float timeMultiplier = 10f;
    float timeScale = 1f;
    public float finalTimeScale = 0f;
    int speedIndexMax = 4;
    public int speedIndex = 0;
    
    public bool isSimulating = false;

    public GameObject playButton, pauseButton;
    public Button speedButton;
    public TextMeshProUGUI daysText, hoursText, minutesText;

    //day night cycle
    float sunriseTime = 6.5f;
    float sunsetTime = 17.5f;
    float halfSunTime = 1f;//time it takes for sun to rise and set

    //time of the day
    public float currentHour = 0f;
    public float currentMinute = 0f;
    public float currentDay = 0f;//amount of days passed

    // Start is called before the first frame update
    void Start()
    {
        isSimulating = false;

        speedButton.onClick.AddListener(ChangeSpeed);

        playButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isSimulating){
            finalTimeScale = Time.deltaTime * timeScale;
            currentMinute += finalTimeScale;
            if(currentMinute >= 60f){
                currentMinute = 0f;
                currentHour += 1f;
            }
            if(currentHour >= 24f){
                currentHour = 0f;
                currentDay += 1f;
            }
        }else{
            finalTimeScale = 0;
        }

        daysText.text = currentDay.ToString("00");
        hoursText.text = currentHour.ToString("00");
        minutesText.text = currentMinute.ToString("00");
    }

    public void ToggleSimulation(){
        isSimulating = !isSimulating;
        playButton.SetActive(!isSimulating);
        pauseButton.SetActive(isSimulating);
    }

    public void ChangeSpeed(){
        speedIndex ++;
        speedIndex %= speedIndexMax;
        switch(speedIndex){
            case 0:
                speedButton.GetComponentInChildren<Text>().text = "x1";
                timeScale = 1f;
                break;
            case 1:
                speedButton.GetComponentInChildren<Text>().text = "x2";
                timeScale = 5f;
                break;
            case 2:
                speedButton.GetComponentInChildren<Text>().text = "x3";
                timeScale = 10f;
                break;
            case 3:
                speedButton.GetComponentInChildren<Text>().text = "x4";
                timeScale = 50f;
                break;
        }
    }

}
