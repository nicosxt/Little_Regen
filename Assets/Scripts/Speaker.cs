using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : Appliance
{


    protected override void OnInitiateAppliance(){
        AudioManager.s.totalSpeakers.Add(this);
        base.OnInitiateAppliance();
    }

    protected override void ToggleAppliance(bool _on){
        AudioManager.s.ToggleSpeaker(_on, this);
        base.ToggleAppliance(_on);
    }
}
