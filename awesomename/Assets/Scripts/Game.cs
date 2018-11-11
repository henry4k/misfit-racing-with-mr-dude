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


    private Dictionary<string, Part> parts = new Dictionary<string, Part>();
    private float startTime;
    
    public Game(References references, Settings settings)
    {
        startTime = Time.time;
        this.settings = settings;
        this.references = references;
        // test
        Part p = new Part();
        p.setName("test");
        p.setCategory("engine");
        p.setValue(100);
        p.setSpriteName("superSprite");
        parts.Add(p.getName(), p);
        Part p2 = new Part();
        p2.name = "p2";
        parts.Add(p2.name, p2);

        // usage example ; SaveGamePath.DataPath is the Projects Asset Folder  
        // SaveGame.Save<Dictionary<string, Part>>("partList", parts); will save the assets to Application.persistentDataPath (%userprofile%\AppData\Local\Packages\<productname>\LocalState.)
        // the asset only saves public variables from custom Data classes, therefore the 'Part' variables are all made public...
        SaveGame.Save<Dictionary<string, Part>>("partList", parts,false, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGamePath.DataPath);
        parts = SaveGame.Load<Dictionary<string, Part>>("partList", parts, false, SaveGame.EncodePassword, SaveGame.Serializer, SaveGame.Encoder, SaveGame.DefaultEncoding, SaveGamePath.DataPath);
        foreach(Part part in parts.Values) {
            Debug.Log(part.name);
        }
    }

    public void Update()
    {

    }
}
