using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour {
    public RaceController raceController;
    public new AudioSource audio;
    public AudioSource lose;
    bool collision = false;
    public Flowchart finish;
    public Flowchart finish2;
    private void OnTriggerEnter2D(Collider2D collision) {
        if(this.collision == false) {
            
            this.collision = true;
            finish.transform.gameObject.SetActive(true);
            //raceController.endOfRace();
            bool hasWon = raceController.hasWon();
            if (hasWon) {
                audio.Play();
                raceController.countdownText.text = "Winner!";
                raceController.main.references.playerReference.player.rank = Utils.currentEnemy.rank;
            }
            else {
                lose.Play();
                raceController.countdownText.text = "Loser!";
            }
            finish2.transform.gameObject.SetActive(true);
        }
    }
}
