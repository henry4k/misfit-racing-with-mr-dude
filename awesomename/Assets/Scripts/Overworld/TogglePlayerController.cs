using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePlayerController : MonoBehaviour {

    public OW_PlayerController playerController;


    private void OnEnable() {
        if (playerController.isActiveAndEnabled) playerController.enabled = false;
        else playerController.enabled = true;
    }
    private void OnDisable() {
        if (playerController.isActiveAndEnabled) playerController.enabled = false;
        else playerController.enabled = true;
    }
}
