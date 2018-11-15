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

    public List<string> categories = new List<string>();

    public Dictionary<string, List<Part>> parts = new Dictionary<string, List<Part>>();
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

        categories.Add("Engine");
        categories.Add("Brake");
        categories.Add("Wheel");
        categories.Add("Body");
        categories.Add("Exhaust");
        // test Scenario setup
        Player p = new Player("John", new Car(), 250, 0);
        references.playerReference.player = p;

        Part engine1 = new Part();
        engine1.id = "0";
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

        Part engine2 = new Part();
        engine2.id = "1";
        engine2.name = "Big Engine";
        engine2.value = 250;
        engine2.spriteName = "bigEngine";
        engine2.category = "Engine";
        Attribute a3 = new Attribute();
        a3.category = "Acceleration";
        a3.value = 3;
        engine2.attributes.Add(a3);
        Attribute a4 = new Attribute();
        a4.category = "MaxSpeed";
        a4.value = 2;
        engine2.attributes.Add(a4);
        engines.Add(engine2);

        p.car.engine = engine1;
        p.partsOwned.Add(engine1);
        //SaveGame.Save<Dictionary<string, List<Part>>>("partList", parts, false, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGamePath.DataPath);
        //parts = SaveGame.Load<Dictionary<string, List<Part>>>("partList", parts, false, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGamePath.DataPath);
    }

    public void Update()
    {

    }

    public void save(int slot) {
        SaveGameObject save = new SaveGameObject();
        save.player = references.playerReference.player;
        save.scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        save.pos = references.playerReference.transform.position;
        SaveGame.Save<SaveGameObject>("Save" + slot + ".sav", save, false, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGamePath.DataPath);
    }

    public void load(int slot) {
        SaveGameObject loaded = new SaveGameObject();
        loaded = SaveGame.Load<SaveGameObject>("Save" + slot + ".sav", loaded, false, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGamePath.DataPath);
        references.playerReference.player = loaded.player;
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(loaded.scene)) {
            references.playerReference.transform.position = loaded.pos;
        } else {
            Utils.isGameLoaded = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene(loaded.scene);
            
        }
    }
}
