using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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

    public List<Enemy> enemies = new List<Enemy>();
    public Enemy brat;
    public Enemy gentleman;
    public Enemy smug;

    public Dictionary<string, List<Part>> parts = new Dictionary<string, List<Part>>();
    private float startTime;
    
    public Game(References references, Settings settings)
    {
        startTime = Time.time;
        this.settings = settings;
        this.references = references;

        Enemy petrolBrat = new Enemy();
        petrolBrat.id = "Hans";
        petrolBrat.rank = 1;
        petrolBrat.map = "RookieMap";
        petrolBrat.timeToBest = 180;
        Car enemyCar = new Car();
        enemyCar.acceleration = 2;
        enemyCar.maxSpeed = 3;
        enemyCar.brakingPower = 2;
        petrolBrat.car = enemyCar;
        enemies.Add(petrolBrat);
        brat = petrolBrat;

        Enemy gasolineGentleman = new Enemy();
        gasolineGentleman.id = "gentleman";
        gasolineGentleman.rank = 2;
        gasolineGentleman.map = "ChampionMap";
        gasolineGentleman.timeToBest = 120;
        enemies.Add(gasolineGentleman);
        gentleman = gasolineGentleman;

        Enemy speedsterSmug = new Enemy();
        speedsterSmug.id = "smug";
        speedsterSmug.rank = 3;
        speedsterSmug.map = "FinalMap";
        speedsterSmug.timeToBest = 105;
        enemies.Add(speedsterSmug);
        smug = speedsterSmug;

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
        
        Player p = new Player("Mr Dude", new Car(), 150, 0);
      

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


        Part exhaust = new Part();
        exhaust.id = "2";
        exhaust.name = "Exhaust pipe";
        exhaust.spriteName = "exhaust";
        exhaust.value = 150;
        exhaust.category = "Exhaust";
        Attribute exAtt = new Attribute();
        exAtt.value = 1;
        exAtt.category = "Acceleration";
        exhaust.attributes.Add(exAtt);
        exhausts.Add(exhaust);

        Part body = new Part();
        body.id = "3";
        body.name = "Car body (nice)";
        body.spriteName = "body";
        body.value = 200;
        body.category = "Body";
        Attribute bodyAttribute = new Attribute();
        bodyAttribute.category = "MaxSpeed";
        bodyAttribute.value = 1;
        body.attributes.Add(bodyAttribute);
        bodies.Add(body);

        Part brake = new Part();
        brake.id = "4";
        brake.name = "brakes";
        brake.value = 150;
        brake.spriteName = "brakes";
        brake.category = "Brake";
        Attribute attBrake = new Attribute();
        attBrake.category = "BreakingPower";
        attBrake.value = 1;
        brake.attributes.Add(attBrake);
        brakes.Add(brake);

        Part brake2 = new Part();
        brake2.id = "5";
        brake2.name = "Brakes the good ones";
        brake2.value = 300;
        brake2.spriteName = "brakesToGoodOnes";
        brake2.category = "Brake";
        Attribute attBrake2 = new Attribute();
        attBrake2.category = "BreakingPower";
        attBrake2.value = 3;
        brake2.attributes.Add(attBrake2);
        brakes.Add(brake2);


        Part wheel = new Part();
        wheel.id = "6";
        wheel.name = "Wheels";
        wheel.spriteName = "wheels";
        wheel.value = 200;
        wheel.category = "Wheel";
        Attribute wMaxSpeed = new Attribute();
        wMaxSpeed.value = 1;
        wMaxSpeed.category = "MaxSpeed";
        Attribute wBrakingPower = new Attribute();
        wBrakingPower.value = 1;
        wBrakingPower.category = "BreakingPower";
        wheel.attributes.Add(wMaxSpeed);
        wheel.attributes.Add(wBrakingPower);
        wheels.Add(wheel);

        Part racingWheels = new Part();
        racingWheels.id = "7";
        racingWheels.name = "Racing wheels";
        racingWheels.value = 500;
        racingWheels.spriteName = "racingWheels";
        racingWheels.category = "Wheel";
        Attribute rwMaxSpeed = new Attribute();
        rwMaxSpeed.value = 2;
        rwMaxSpeed.category = "MaxSpeed";
        Attribute rwAcceleration = new Attribute();
        rwAcceleration.value = 1;
        rwAcceleration.category = "Acceleration";
        Attribute rwBrakingPower = new Attribute();
        rwBrakingPower.value = 2;
        rwBrakingPower.category = "BreakingPower";
        racingWheels.attributes.Add(rwMaxSpeed);
        racingWheels.attributes.Add(rwBrakingPower);
        racingWheels.attributes.Add(rwAcceleration);
        wheels.Add(racingWheels);

        p.car.engine = engine1;
        p.partsOwned.Add(engine1);
        references.playerReference.player = p;
    }
    public void Update() {

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
        if (loaded.player == null && loaded.scene == null) return;
        references.playerReference.player = loaded.player;
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(loaded.scene)) {
            references.playerReference.transform.position = loaded.pos;
        } else {
            Utils.isGameLoaded = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene(loaded.scene);
        }
    }

    public void toWorkshop() {
        SaveGameObject save = new SaveGameObject();
        save.player = references.playerReference.player;
        save.pos = references.playerReference.transform.position;
        Utils.currentSave = save;
    }

    public void toRaceTrack() {
        SaveGameObject save = new SaveGameObject();
        save.player = references.playerReference.player;
        save.pos = references.playerReference.transform.position;
        Utils.currentSave = save;
    }
}
