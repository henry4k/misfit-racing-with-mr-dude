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

    public string getSpriteName() { return this.spriteName; }
    public void setSpriteName(string spriteName) { this.spriteName = spriteName; }

    public string getName() { return name; }
    public void setName(string name) { this.name = name; }

    public string getCategory() { return category; }
    public void setCategory(string category) { this.category = category; }

    public int getValue() { return value; }
    public void setValue(int value) { this.value = value; }

    public List<Attribute> getAttributes() { return attributes; }
    public void setAttributes(List<Attribute> attributes) { this.attributes = attributes; }

}
