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
            rank = "Just got his licence.";
        } else if(playerNumberRank == 1) {
            rank = "Won a race, \nthinks he is the greatest.";
        } else if(playerNumberRank == 2) {
            rank = "Won two races a real Champion.";
        } else if (playerNumberRank == 3) {
            rank = "Won all races. \nClaims to be the \"new\" Ricky Bobby.";
        }

        text.text = "Rank: \n" + rank;

    }
}
