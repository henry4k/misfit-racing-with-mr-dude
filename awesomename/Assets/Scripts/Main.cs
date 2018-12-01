using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Main : MonoBehaviour
{
    public References references;
    public Settings settings;

    Game game;

    private void Awake() {
        game = new Game(references, settings);
        if(Utils.isGameLoaded) {
            game.load(1);
            Utils.isGameLoaded = false;
        } else {
            if(Utils.currentSave != null) {
                references.playerReference.player = Utils.currentSave.player;
                references.playerReference.transform.position = Utils.currentSave.pos;
            }
        }

    }

    void Start()
    {

#if UNITY_EDITOR
        UnityEngine.Random.InitState(1337);
#endif
        
    }

    void Update()
    {
        if (game != null)
        {
            game.Update();
        }
    }

    public Game getGame() { return game; }
}