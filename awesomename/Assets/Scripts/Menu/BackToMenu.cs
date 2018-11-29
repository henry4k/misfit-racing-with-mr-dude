using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour {
    public GameObject menu;
    public Main main;
    public new AudioSource audio;
    public void goBack() {
        menu.SetActive(true);
        gameObject.SetActive(false);
    }
    private void Update() {
        if (Input.GetKeyUp(main.settings.Enter)) {
            audio.Play();
            goBack();
        }
    }
    
}
