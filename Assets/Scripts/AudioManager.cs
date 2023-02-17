using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager s;
    void Awake() {
        if (s != null && s != this) {
            Destroy(this.gameObject);
        } else {
            s = this;
        }
    }

    public AudioSource mainAudioSource;
    public AudioClip[] audioClips;
    public List<Speaker> totalSpeakers = new List<Speaker>();
    public List<Speaker> speakersOn = new List<Speaker>();
    float volumePerSpeaker;
    public bool isPlaying = false;

    bool canPlay = false;
    // Start is called before the first frame update
    void Start()
    {
        mainAudioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSpeaker(bool _on, Speaker _speaker){
        //add / remove speaker to speakersOn
        if(_on){
            speakersOn.Add(_speaker);
        } else {
            speakersOn.Remove(_speaker);
        }

        //if speaker is turned off and no speaker is on
        if(speakersOn.Count == 0){
            mainAudioSource.Stop();
            return;
        }

        mainAudioSource.volume = (float) speakersOn.Count / (float) totalSpeakers.Count;

        //when a speaker is turned on and no music is playing
        if(!mainAudioSource.isPlaying && _on){
            mainAudioSource.Play();
        }

    }

    public void PlayAudio(bool _toggle){
    }

    public void ToggleSimulation(bool _canPlay){
        canPlay = _canPlay;
        PlayAudio(_canPlay);
    }
}
