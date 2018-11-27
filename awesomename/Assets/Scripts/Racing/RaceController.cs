using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceController : MonoBehaviour {
    public Main main;
    public Rigidbody2D mapBody;
    public Text countdownText;
    public Text raceTimerText;
    public Text timeToBeat;
    public List<GameObject> obstacles = new List<GameObject>();
    public AudioSource timerAudio;
    public AudioSource timerFinishAudio;
    public float maxSpeedOffset = 1f;
    bool isLeaving = false;
    [Space(20)]
    public List<GameObject> maps = new List<GameObject>();
    public int fuelCollected = 0;
    [HideInInspector]
    public bool isRacing = false;
    private bool isStarting = true;
    List<string> countdown = new List<string>();
    int countDownIndex = 0;
    private float referenceTime = 150; // time it takes for a perfect race in seconds with maxSpeed 1 and maxSpeedOffset = 1
    private float raceTime = 0;
    private float raceStartTime = 0;
    private PlayerController playerController;
    private GameObject currentMap;
    private bool startup = true;
    // Use this for initialization
    void Start () {
        countdown.Add("2");
        countdown.Add("1");
        countdown.Add("Go!");
        playerController = main.references.playerReference.playerController;
        string map = Utils.currentEnemy.map;
        foreach(GameObject m in maps) {
            if (m.name.Equals(map)) {
                m.SetActive(true);
                currentMap = m;
                mapBody = m.GetComponent<Rigidbody2D>();
            }
        }
        timeToBeat.text = "Time to beat: \n";
        timeToBeat.text += updateUITimer(Utils.currentEnemy.timeToBest);
        StartCoroutine(doCountdown());
    }
	
    public void endOfRace() {
        isRacing = false;
        float timePassed = raceTime - raceStartTime;
        if (timePassed < Utils.currentEnemy.timeToBest) {
            countdownText.text = "Winner!";
            main.references.playerReference.player.rank = Utils.currentEnemy.rank;
        }
        else {
            countdownText.text = "Loser!";
        }
        main.references.playerReference.player.money += fuelCollected;
        countdownText.transform.gameObject.SetActive(true);
    }

    private IEnumerator doCountdown() {
        while(countDownIndex < countdown.Count) {
            timerAudio.Play();
            yield return new WaitForSeconds(1);
            countdownText.text = countdown[countDownIndex];
            countDownIndex++;
        }
        timerFinishAudio.Play();
        yield return new WaitForSeconds(1);
        
        countdownText.transform.gameObject.SetActive(false);
        isRacing = true;
        startRaceTimer();
        isStarting = false;
    }

    private void startRaceTimer() {
        raceStartTime = Time.timeSinceLevelLoad;
       
    }
    private void OnEnable() {
        if(startup) {
            startup = false;
            return;
        }
        if(isStarting) {
            StartCoroutine(doCountdown());
        }
    }
    // Update is called once per frame
    void Update () {
        if(isRacing) {
            raceTime = Time.timeSinceLevelLoad;
            float timePassed = raceTime - raceStartTime;
            raceTimerText.text = updateUITimer(timePassed);

            if (Input.GetKeyDown(main.settings.D)) {
                playerController.moveToSide(true);
            }
            if (Input.GetKeyDown(main.settings.A)) {
                playerController.moveToSide(false);
            }
        }
    }

    private string updateUITimer(float timePassed) {
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
        return "" + m + ":" + s + ":" + mili;
    }

    private void FixedUpdate() {
        if(isRacing) {
            float moveVertical = Input.GetAxisRaw("Vertical");
            playerController.accelerate(moveVertical);
            if (mapBody.velocity.y < -main.references.playerReference.player.car.maxSpeed - maxSpeedOffset) {
            }
            else {
                if (currentMap.transform.position.y > 0 && playerController.velocity.y < 0) {
                    mapBody.velocity = Vector2.zero;
                    return;
                }
                mapBody.AddForce(-playerController.velocity);
            }
        } else {
            if(!isStarting && !isLeaving) StartCoroutine(leave());
        }
    }

    public IEnumerator leave() {
        isLeaving = true;
        yield return new WaitForSeconds(2);
        if (Utils.currentSave == null) {
            SaveGameObject currentSave = new SaveGameObject();
            currentSave.player = main.references.playerReference.player;
            Utils.currentSave = currentSave;
        }
        else {
            Utils.currentSave.player = main.references.playerReference.player;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("Overworld");
    }
}
