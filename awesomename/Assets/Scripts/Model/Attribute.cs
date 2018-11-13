using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute {

    public string category;
    public int value;

    public Attribute() {

    }

    public Attribute(string category, int value) {
        this.category = category;
        this.value = value;
    }
    
    public Attribute copy() {
        Attribute copy = new Attribute();
        copy.category = category;
        copy.value = value;

        return copy;
    }
}
