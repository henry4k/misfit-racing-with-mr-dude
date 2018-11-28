using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDialog : Npc {
    public Main main;
    
    public new void activateDialog() {

        dialog.SetActive(true);
    }
}
