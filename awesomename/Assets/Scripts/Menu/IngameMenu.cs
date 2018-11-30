using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameMenu : MonoBehaviour {
    public Main main;
    public new AudioSource audio;
    public AudioSource audioClick;
    public List<GameObject> toDisableWhileOpen = new List<GameObject>();
    private int index;
    public int restart = 0;
    public List<GameObject> buttons = new List<GameObject>();
    public GameObject credits;

    public bool mainMenu = false;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyUp(main.settings.W)) {
            nav(true);
        }
        if (Input.GetKeyUp(main.settings.S)) {
            nav(false);
        }
        if (Input.GetKeyUp(main.settings.Enter)) {
            audioClick.Play();
            if(!mainMenu) {
                if (Utils.mod(index, buttons.Count) == 0) {
                    OnResume();
                }
                else if (restart != 0 && Utils.mod(index, buttons.Count) == 1) {
                    OnRestart();
                }
                else if (restart == 0 && Utils.mod(index, buttons.Count) == 1) {
                    OnSave();
                }
                else if (restart == 0 && Utils.mod(index, buttons.Count) == 2) {
                    OnLoad();
                }
                else if (restart == 0 && Utils.mod(index, buttons.Count) == 3) {
                    OnQuit();
                }
                else if (restart != 0 && Utils.mod(index, buttons.Count) == 2) {
                    OnQuit();
                }
            } else {
               
                if (Utils.mod(index, buttons.Count) == 0) {
                    OnNewGame();
                }
                else if (restart == 0 && Utils.mod(index, buttons.Count) == 1) {
                    OnLoad();
                }
                else if (restart == 0 && Utils.mod(index, buttons.Count) == 2) {
                    OnCredits();
                }
                else if (restart == 0 && Utils.mod(index, buttons.Count) == 3) {
                    OnQuit();
                }
                else if (restart != 0 && Utils.mod(index, buttons.Count) == 2) {
                    OnQuit();
                }
            }
          
        }
    }

    private void nav(bool up) {
        audio.Play();
        buttons[index].transform.GetChild(1).transform.gameObject.SetActive(false);
        if(!up) index = Utils.mod(index + 1, buttons.Count);
        else index = Utils.mod(index - 1, buttons.Count);
        buttons[index].transform.GetChild(1).transform.gameObject.SetActive(true);
    }

    public void OnRestart() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Racing");
    }

    public void OnResume() {
        foreach (GameObject o in toDisableWhileOpen) {
            o.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    public void OnNewGame() {
        Utils.isNewGameStart = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Overworld");
    }

    public void OnSave() {
        main.getGame().save(1);
        OnResume();
    }

    public void OnLoad() {
        main.getGame().load(1);
        OnResume();
    }

    public void OnCredits() {
        credits.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnQuit() {
        Application.Quit();
    }
}
