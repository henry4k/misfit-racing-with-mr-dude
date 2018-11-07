using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using DG.Tweening;
using UnityEngine;

public class Game
{
    private Settings settings;
    private References references;

    private float startTime;
    
    public Game(References references, Settings settings)
    {
        startTime = Time.time;
        this.settings = settings;
        this.references = references;
       


    }

    public void Update()
    {

    }
}
