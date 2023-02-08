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
    public float fractionMinutes = 0f;
    public float totalMinutes = 0f;

    public float timeScale = 1f;
    public float finalTimeScale = 0f;
    
    public bool isSimulating = false;

    public GameObject playButton, pauseButton;
    public TextMeshProUGUI hoursText, minutesText;

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
            finalTimeScale = Time.deltaTime * timeScale * timeMultiplier;
            fractionMinutes += finalTimeScale;
            totalMinutes += finalTimeScale;
            if(fractionMinutes >= 60f){
                totalHours += 1f;
                fractionMinutes = 0f;
            }
        }else{
            finalTimeScale = 0;
        }


        hoursText.text = totalHours.ToString("F0");
        minutesText.text = fractionMinutes.ToString("F0");
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
