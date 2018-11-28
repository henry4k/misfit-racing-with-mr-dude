using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwController : MonoBehaviour {
    public Main main;
    public Flowchart enemyBrat;
    public Flowchart enemyGentleman;
    public Flowchart enemySmug;
    private void Start() {
        enemyBrat.SetIntegerVariable("PlayerRank", main.references.playerReference.player.rank);
        enemyBrat.SetIntegerVariable("EnemyRank", main.getGame().brat.rank);

        enemyGentleman.SetIntegerVariable("PlayerRank", main.references.playerReference.player.rank);
        enemyGentleman.SetIntegerVariable("EnemyRank", main.getGame().gentleman.rank);

        enemySmug.SetIntegerVariable("PlayerRank", main.references.playerReference.player.rank);
        enemySmug.SetIntegerVariable("EnemyRank", main.getGame().smug.rank);
    }
    public void startRaceBrat() {
        Utils.currentEnemy = main.getGame().brat;
        main.getGame().toRaceTrack();
    }
    public void startRaceGentleman() {
        Utils.currentEnemy = main.getGame().gentleman;
        main.getGame().toRaceTrack();
    }

    public void startRaceSmug() {
        Utils.currentEnemy = main.getGame().smug;
        main.getGame().toRaceTrack();
    }

}
