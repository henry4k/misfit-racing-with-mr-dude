using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceController : MonoBehaviour {
    public GameObject map;
    public Main main;
    public Rigidbody2D mapBody;
    public Text countdownText;
    public Text raceTimerText;
    public List<GameObject> obstacles = new List<GameObject>();
    public AudioSource timerAudio;
    public AudioSource timerFinishAudio;
    private PlayerController playerController;
    public float maxSpeedOffset = 1f;
    private float raceTime = 0;
    private float raceStartTime = 0;
    [HideInInspector]
    public bool isRacing = false;
    private float referenceTime = 150; // time it takes for a perfect race in seconds with maxSpeed 1 and maxSpeedOffset = 1
    // Use this for initialization
    void Start () {
        playerController = main.references.playerReference.playerController;
        StartCoroutine(countdown());
    }
	
    private IEnumerator countdown() {
        timerAudio.Play();
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        timerAudio.Play();
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        timerAudio.Play();
        yield return new WaitForSeconds(1);
        countdownText.text = "Go!";

        timerFinishAudio.Play();
        yield return new WaitForSeconds(1);
        countdownText.transform.gameObject.SetActive(false);
        isRacing = true;
        startRaceTimer();
    }

    private void startRaceTimer() {
        raceStartTime = Time.timeSinceLevelLoad;
        //StartCoroutine(raceTimer());
    }
    private IEnumerator raceTimer() {
        while(isRacing) {
            raceTime = Time.timeSinceLevelLoad;
            int minutes = (int)((raceTime - raceStartTime) / 60f);
            int seconds = (int)((raceTime - raceStartTime) % 60f);
            int milliseconds = (int)((raceTime - raceStartTime) * 6f);
            raceTimerText.text = "" + minutes + ":" + seconds + ":" + milliseconds;
            yield return new WaitForSeconds(0.1f);
        }
       
    }
	// Update is called once per frame
	void Update () {
       
        if(isRacing) {
            raceTime = Time.timeSinceLevelLoad;
            float timePassed = raceTime - raceStartTime;
            updateUITimer(timePassed);

            if (Input.GetKeyDown(main.settings.D)) {
                playerController.moveToSide(true);
            }
            if (Input.GetKeyDown(main.settings.A)) {
                playerController.moveToSide(false);
            }
            if (Input.GetKeyUp(KeyCode.E)) {

            }
        }
       
    }

    private void updateUITimer(float timePassed) {
        int minutes = (int)(timePassed / 60f) % 60;
        string m = "";
        if (minutes < 10) m += "0";
        m += minutes;
        int seconds = (int)(timePassed % 60f) % 60;
        string s = "";
        if (seconds < 10) s += "0";
        s += seconds;
        int milliseconds = (int)(timePassed * 1000f) % 1000;
        string mili = "";
        if (milliseconds < 100) mili += "0";
        if (milliseconds < 10) mili += "0";
        mili += milliseconds;
        raceTimerText.text = "" + m + ":" + s + ":" + mili;
    }

    private void FixedUpdate() {
        if(isRacing) {
            float moveVertical = Input.GetAxisRaw("Vertical");
            playerController.accelerate(moveVertical);
            if (mapBody.velocity.y < -main.references.playerReference.player.car.maxSpeed - maxSpeedOffset) {
            }
            else {
                mapBody.AddForce(-playerController.velocity);
            }
        }
        
    }
}
