using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// The event class handles the activation logic of events. 
/// It does not have an action implemented.
/// You can use this class to activate other events.
/// 
/// </summary>
public class EventClass : MonoBehaviour {
    [System.Serializable]
    public class nextEvent {
        public GameObject next;
        public int time = 0;
    }
    [Tooltip("Wenn aktiviert, wird das Ereignis bei einer Kollision mit dem Spieler ausgelöst.")]
    public bool OnCollision;
    [Tooltip("Wenn aktiviert, wird das Ereignis bei einer Aktivierung des Gameobjekts ausgelöst.")]
    public bool OnActivation;
    [Tooltip("Sorgt dafür, dass bei OnActivation das Ereignis bei Start der Szene aufgeführt wird.")]
    public bool OnSceneStart = false;
    [Tooltip("Wenn aktiviert, wird das Ereignis nur ein Mal ausgeführt.")]
    public bool SingleUse = false;
    [Tooltip("Option wirkt sich nur auf die Aktivierung durch den EventManager aus. Wenn aktiviert, wird das Event nur ausgeführt wenn der Mittelpunkt des Spieler in der Nähe des Events ist.")]
    public bool DistanceControlled = false;
    [Tooltip("Maximale Entfernung des Spielers zum Event. Ist der Spieler weiter weg wird das Event nicht mehr ausgeführt.")]
    public float distance = 2;
    [Space(10)]
    [Tooltip("Hier werden die Ziele des Ereignisses eingetragen. Auf sie wird die jeweilige Aktion ausgeführt.")]
    public List<GameObject> targets = new List<GameObject>();
    [Tooltip("Hier werden die Ereignisse eingetragen, die ausgeführt werden sollen, sobald dieses Ereignis fertig ist." +
        "Bei 'Zeit' wird eingetragen, mit wie viel Sekunden Verzögerung, das nächste Ereignis gestartet wird.")]
    public List<nextEvent> nextEventsList = new List<nextEvent>();
    
    protected float waitBeforeAction = 0;
    private float maxWait = 0;
    protected bool lockAction = false;
    protected bool finished = false;
    private bool initialized = false;
   
    // Events with no fixed end time have to set this value in their action method
    [HideInInspector()]
    public bool actionFinished = false;
    // is needed for restart if event is deactivated while in action
    [HideInInspector()]
    public GameObject eCopy;
    // Events that are not instant have to turn this off in their actionImpl
    [HideInInspector()]
    public bool instant = true;

    void OnTriggerEnter(Collider other) {
        if (OnCollision && lockAction == false && !finished) {
            if (other.gameObject.tag == "Player") {
                StartCoroutine(action());
                lockAction = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (OnCollision && lockAction == false && !finished) {
            if (collision.gameObject.tag == "Player") {
                StartCoroutine(action());
                lockAction = true;
            }
        }
    }

    void OnEnable() {
        if(!OnSceneStart && !initialized && Time.timeSinceLevelLoad == 0) {
            initialized = true;
            return;
        }
        if (OnActivation && lockAction == false && !finished) {
            StartCoroutine(action());
            lockAction = true;
        }
        if(lockAction == true && !finished && !actionFinished) {
            if (eCopy != null) {
                eCopy.SetActive(true);
                StartCoroutine(restart());
            }
        }
    }

    private void OnDisable() {
        if (eCopy != null) eCopy.SetActive(false);
    }
    /// <summary>
    /// Is called from the EventManager to fire the event.
    /// </summary>
    public void callAction() {
        if (lockAction == false && !finished) {
            if(DistanceControlled) {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance > this.distance) return;
            }
            StartCoroutine(action());
            lockAction = true;
        }
    }

    private IEnumerator action() {
        if (!lockAction) {
            yield return new WaitForSeconds(waitBeforeAction);
            eCopy = runActionImpl();
            
            Type t = GetType();
            object e = eCopy.GetComponent(t);
            FieldInfo pInfo = e.GetType().GetField("instant");
            bool b = (bool)pInfo.GetValue(e);
            if (!b) {
                yield return StartCoroutine(waitForActionToFinish(e));
            }
            e.GetType().GetField("actionFinished").SetValue(e, true);
            e.GetType().GetMethod("TriggerNextEvents").Invoke(e, null);
            if (SingleUse) {
                finished = true;
            } 
        }
        lockAction = false;
        yield return null; 
    }

    // Event implementations have to override this function.
    public virtual void actionImpl() {
        
    }

    public void callActionFinished() {
        if(!actionFinished) {
            actionFinished = true;
            TriggerNextEvents();
            if (SingleUse) {
                finished = true;
            }
        }
    }

    private IEnumerator restart() {
        Type t = GetType();
        object e = eCopy.GetComponent(t);
        yield return StartCoroutine(waitForActionToFinish(e));
        e.GetType().GetMethod("TriggerNextEvents").Invoke(e, null);
        if (SingleUse) {
            finished = true;
        }
        lockAction = false;
        yield return null;
    }

    private IEnumerator waitForActionToFinish(object o) {
        FieldInfo pInfo = o.GetType().GetField("actionFinished");
        bool f = (bool)pInfo.GetValue(o);
        while (!f) {
            yield return new WaitForSeconds(0.025f);
            pInfo = o.GetType().GetField("actionFinished");
            f = (bool)pInfo.GetValue(o);
        }
    }

    private GameObject  runActionImpl() {
        Type t = GetType();
        GameObject newEventObject = new GameObject(" runActionImpl" , t);
        object e = newEventObject.GetComponent(t);
        e.GetType().GetMethod("copy").Invoke(e, new[] {this });
        e.GetType().GetMethod("actionImpl").Invoke(e, null);
        return newEventObject;
    }

    public void TriggerNextEvents() {
        if(nextEventsList != null) {
			Type t = GetType();
			object e = gameObject.GetComponent(t);
            if(e == null) Debug.Log("e is null");
            FieldInfo pInfo = e.GetType().GetField("nextEventsList");
            if (pInfo == null) Debug.Log("pInfo is null");
            object list = pInfo.GetValue(e);
            if (list == null) Debug.Log("list is null");
            List<nextEvent> nextList = (List<nextEvent>)list;
            foreach (nextEvent n in nextList ) {
                if (n == null || n.next == null) continue;
                EventClass eventScript = n.next.GetComponent<EventClass>();
                // already running event has to be destroyed if parent event is activated a second time
                if (eventScript.eCopy != null) Destroy(eventScript.eCopy);
                eventScript.setTimeBeforeAction(n.time);
                if (maxWait < n.time) maxWait = n.time;
                if (!eventScript.finished) StartCoroutine(eventScript.action());
            }
        }
        Destroy(gameObject, maxWait + 0.1f);
    }

    public void setTimeBeforeAction(float time) {
        waitBeforeAction = time;
    }

    public virtual void copy(EventClass e) {
        this.OnCollision = e.OnCollision;
        this.OnActivation = e.OnActivation;
        this.SingleUse = e.SingleUse;
        foreach(GameObject o in e.targets) {
            targets.Add(o);
        }
        foreach(nextEvent n in e.nextEventsList) {
            nextEventsList.Add(n);
        }

        waitBeforeAction = e.waitBeforeAction;
        maxWait = e.maxWait;
        lockAction = e.lockAction;
        finished = e.finished;
        DistanceControlled = e.DistanceControlled;
        distance = e.distance;
    }
}
