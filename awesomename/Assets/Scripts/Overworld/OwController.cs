using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwController : MonoBehaviour {
    public Main main;
    public Flowchart enemyBrat;
    private void Start() {
        enemyBrat.SetIntegerVariable("PlayerRank", main.references.playerReference.player.rank);
        enemyBrat.SetIntegerVariable("EnemyRank", main.getGame().brat.rank);
    }
    public void startRaceBrat() {
        Utils.currentEnemy = main.getGame().brat;
        main.getGame().toRaceTrack();
    }


}
