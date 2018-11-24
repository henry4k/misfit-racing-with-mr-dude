using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    public RaceController raceController;
    public GameObject collisionEffect;
    private new AudioSource audio;
    bool collision = false;
	// Use this for initialization
	void Start () {
        audio = transform.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (this.collision == false) {
            this.collision = true;
            collisionEffect.SetActive(true);
            raceController.mapBody.velocity = Vector2.zero;
            transform.GetChild(0).gameObject.SetActive(true);
            audio.Play();
        }
    }
}
