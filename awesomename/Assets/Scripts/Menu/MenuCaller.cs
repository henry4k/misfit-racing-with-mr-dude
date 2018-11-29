using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCaller : MonoBehaviour {
    public Main main;
    public IngameMenu ingameMenu;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if(main != null) {
            if (Input.GetKeyUp(main.settings.Esc)) {
                GameObject panel = transform.GetChild(0).gameObject;
                if (panel.activeSelf) {
                    panel.SetActive(false);
                    foreach (GameObject o in ingameMenu.toDisableWhileOpen) {
                        o.SetActive(true);
                    }
                }
                else {
                    panel.SetActive(true);
                    foreach (GameObject o in ingameMenu.toDisableWhileOpen) {
                        o.SetActive(false);
                    }
                }
            }
        }
        
    }
}
