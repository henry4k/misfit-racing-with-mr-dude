using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part {
    public string id;
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

    public Part copy() {
        Part copy = new Part();
        copy.id = id;
        copy.name = name;
        copy.category = category;
        copy.value = value;
        copy.spriteName = spriteName;

        foreach(Attribute attribute in attributes) {
            copy.attributes.Add(attribute.copy());
        }

        return copy;
    }
}
