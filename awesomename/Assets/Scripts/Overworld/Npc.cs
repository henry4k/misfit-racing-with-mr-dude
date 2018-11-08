using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// i am npc
public class Npc : MonoBehaviour {
    public GameObject dialog;

    public void activateDialog() {
        dialog.SetActive(true);
    }
}
