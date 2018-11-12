using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using DG.Tweening;
using UnityEngine;
using BayatGames.SaveGameFree;
public class Game
{
    private Settings settings;
    private References references;

    private List<Part> engines = new List<Part>();
    private List<Part> brakes = new List<Part>();
    private List<Part> wheels = new List<Part>();
    private List<Part> bodies = new List<Part>();
    private List<Part> exhausts = new List<Part>();

    private Dictionary<string, List<Part>> parts = new Dictionary<string, List<Part>>();
    private float startTime;
    
    public Game(References references, Settings settings)
    {
        startTime = Time.time;
        this.settings = settings;
        this.references = references;

        // Categories are the string keys
        parts.Add("Engine", engines);
        parts.Add("Brake", brakes);
        parts.Add("Wheel", wheels);
        parts.Add("Body", bodies);
        parts.Add("Exhaust", exhausts);

        // test Scenario setup
        Player p = new Player("John", new Car(), 100, 0);
        references.playerReference.player = p;

        Part engine1 = new Part();
        engine1.name = "Small Engine";
        engine1.value = 100;
        engine1.spriteName = "smallEngine";
        engine1.category = "Engine";
        Attribute a1 = new Attribute();
        a1.category = "Acceleration";
        a1.value = 1;
        engine1.attributes.Add(a1);
        Attribute a2 = new Attribute();
        a2.category = "MaxSpeed";
        a2.value = 1;
        engine1.attributes.Add(a2);
        engines.Add(engine1);

        //SaveGame.Save<Dictionary<string, List<Part>>>("partList", parts, false, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGamePath.DataPath);
        //parts = SaveGame.Load<Dictionary<string, List<Part>>>("partList", parts, false, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGamePath.DataPath);
    }

    public void Update()
    {

    }
}
