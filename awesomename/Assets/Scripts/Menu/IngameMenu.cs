using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameMenu : MonoBehaviour {
    public Main main;

    public List<GameObject> toDisableWhileOpen = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnResume() {
        foreach (GameObject o in toDisableWhileOpen) {
            o.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    public void OnSave() {
        main.getGame().save(1);
    }

    public void OnLoad() {
        main.getGame().load(1);
    }

    public void OnQuit() {
        Application.Quit();
    }
}
