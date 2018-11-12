using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part {

    public string name;
    public string category;
    public int value;
    public string spriteName;
    public List<Attribute> attributes = new List<Attribute>();

    public Part() {

    }

    public Part(string name, string category, int value, string spriteName) {
        this.name = name;
        this.category = category;
        this.value = value;
        this.spriteName = spriteName;
    }

    public Part(string name, string category, int value, string spriteName, List<Attribute> attributes) {
        this.name = name;
        this.category = category;
        this.value = value;
        this.spriteName = spriteName;
        this.attributes = attributes;
    }
}
