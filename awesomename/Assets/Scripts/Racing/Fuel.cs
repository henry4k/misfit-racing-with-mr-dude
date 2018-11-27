using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour {
    public RaceController raceController;
    public int amount = 50;
    private new AudioSource audio;
    bool collision = false;
    // Use this for initialization
    void Start() {
        audio = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (this.collision == false) {
            this.collision = true;
            transform.GetChild(0).gameObject.SetActive(true);
            raceController.fuelCollected += amount;
            audio.Play();
        }
    }
}
