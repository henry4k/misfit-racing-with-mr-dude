using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute {

    private string category;
    private int value;

    public Attribute() {

    }

    public Attribute(string category, int value) {
        this.category = category;
        this.value = value;
    }

    public string getCategory() { return category; }
    public void setCategory(string category) { this.category = category; }

    public int getValue() { return value; }
    public void setValue(int value) { this.value = value; }
}
