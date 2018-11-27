using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Money : MonoBehaviour {
    public Main main;
    public Text text;
    public RaceController raceController;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(raceController != null) text.text = main.references.playerReference.player.money  + raceController.fuelCollected + "L";
        else text.text = main.references.playerReference.player.money + "L";
    }
}
