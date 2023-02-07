using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public float totalHours = 0f;
    public float totalMinutes = 0f;

    private float timeScale = 1f;
    
    public bool isSimulating = false;

    public GameObject playButton, pauseButton;
    public TextMeshProUGUI totalHoursText, totalMinutesText;

    // Start is called before the first frame update
    void Start()
    {
        isSimulating = false;
        SetTimeScale(0.5f);
        playButton.SetActive(true);
        pauseButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isSimulating){
            totalMinutes += Time.deltaTime * timeScale * timeMultiplier;
            if(totalMinutes >= 60f){
                totalHours += 1f;
                totalMinutes = 0f;
            }
        }


        totalHoursText.text = totalHours.ToString("F0");
        totalMinutesText.text = totalMinutes.ToString("F0");
    }

    public void ToggleSimulation(){
        isSimulating = !isSimulating;
        playButton.SetActive(!isSimulating);
        pauseButton.SetActive(isSimulating);
    }

    public void SetTimeScale(float _timeScale){
        timeScale = 1f + _timeScale * timeMultiplier;
    }
}
