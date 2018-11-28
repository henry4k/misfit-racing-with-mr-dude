using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlayerController : MonoBehaviour {

    public OW_PlayerController playerController;


    private void OnEnable() {
        try {
            if (playerController.isActiveAndEnabled) playerController.enabled = false;
            else playerController.enabled = true;
        }
        catch (Exception e) {

        }
    }
    private void OnDisable() {
        try {
            if (playerController.isActiveAndEnabled) playerController.enabled = false;
            else playerController.enabled = true;
        } catch(Exception e) {

        }
        
    }
}
