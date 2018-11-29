using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rank : MonoBehaviour {
    public Text text;
    public Main main;

    private void Start() {
        string rank = "";
        int playerNumberRank = main.references.playerReference.player.rank;
        if(playerNumberRank == 0) {
            rank = "Rookie";
        } else if(playerNumberRank == 1) {
            rank = "Champion";
        } else if(playerNumberRank == 2) {
            rank = "Master";
        }

        text.text = "Rank: " + rank;

    }
}
