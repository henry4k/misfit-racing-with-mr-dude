using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {
    public RaceController raceController;
    public new AudioSource audio;
    bool collision = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(this.collision == false) {
            audio.Play();
            this.collision = true;
            raceController.endOfRace();
        }
    }
}
