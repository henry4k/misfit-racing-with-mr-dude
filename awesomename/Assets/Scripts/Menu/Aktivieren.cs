using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class uses the Event as base class and implements the action: activate all target Gameobjects.
/// </summary>
public class Aktivieren : EventClass{

   public override void actionImpl() {
        activateTargets();
    }

    private void activateTargets() {
        foreach(GameObject o in targets) {
            if (o != null) o.SetActive(true);
        }
    }
}
