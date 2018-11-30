using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bewegen : EventClass {
    [Space(10)]
    [Tooltip("Hier das zu bewegende Gameobjekt eintragen. Wenn leergelassen, wird das Eltern-Objekt des Scripts bewegt! Die Wegpunkte bei \"Targets\" eintragen.")]
    public GameObject toMove;
    [Tooltip("Bewegungsgeschwindigkeit.")]
    public float speed = 1;
    [Tooltip("Soll das Gameobjekt sein Ziel anschauen.")]
    public bool rotate = true;
    public bool loop;
    public bool jumpBackToStart = false;
    private bool start;
    private int state = 0;
    private bool jumpToStart = true;
    private void Awake() {
        if (toMove == null) toMove = gameObject;
    }
    public override void actionImpl() {
        start = true;
        instant = false;
    }

    private void Update() {
        if(start) {
            move();
        }
    }

    private void move() {
        if(targets == null || targets.Count <= 1) {
            Debug.Log("Bewegen Ereignis hat zu wenig Wegpunkte.");
            return;
        }
        if (targets.Count > state) {
            if(jumpToStart && state == 0) {
                toMove.transform.position = targets[state].transform.localPosition; 
                state++;
                jumpToStart = false;
            }
            GameObject currentTarget = targets[state];
            if (currentTarget != null) {
                float step = speed * Time.deltaTime;
                toMove.transform.position = Vector3.MoveTowards(toMove.transform.position, currentTarget.transform.position, step);
                if(rotate) {
                    toMove.transform.Rotate(new Vector3(0, 0, 0.5f));
                    Vector3 targetDir = currentTarget.transform.position - toMove.transform.position;
                    targetDir.Normalize();
                    Vector3 newDir = Vector3.RotateTowards(toMove.transform.forward, targetDir, step, 0.0f);
                    //toMove.transform.rotation = Quaternion.LookRotation(newDir);
                }
              
                if (toMove.transform.position == currentTarget.transform.position) {
                    state++;
                }
            }
        } else {
            if(loop) {
                state = 0;
                GameObject currentTarget = targets[state];
                jumpToStart = jumpBackToStart;
            } else {
                callActionFinished();
            }
        }
    }

    public override void copy(EventClass e) {
        base.copy(e);
        Bewegen bewegen = (Bewegen)e;
        this.speed = bewegen.speed;
        rotate = bewegen.rotate;
        start = bewegen.start;
        state = bewegen.state;
        toMove = bewegen.toMove;
        loop = bewegen.loop;
        jumpBackToStart = bewegen.jumpBackToStart;
    }
}
