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

    void Start()
    {
        DOTween.Init();
#if UNITY_EDITOR
        UnityEngine.Random.InitState(1337);
#endif
        game = new Game(references,settings);
    }

    void Update()
    {
        if (game != null)
        {
            game.Update();
        }
    }
}