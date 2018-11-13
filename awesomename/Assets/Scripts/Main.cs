using System;
using System.Collections.Generic;
using DG.Tweening;
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
    }

    void Start()
    {
        DOTween.Init();
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