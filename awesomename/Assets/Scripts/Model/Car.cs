using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car {

    public int hp;

    // attributes
    public int acceleration;
    public int maxSpeed;
    public int brakingPower;

    public Part engine;
    public Part brake;
    public Part wheel;
    public Part body;
    public Part exhaust;
    
    public Car copy() {
        Car copy = new Car();
        copy.hp = this.hp;
        copy.acceleration = acceleration;
        copy.maxSpeed = maxSpeed;
        copy.brakingPower = brakingPower;

        if (engine != null) copy.engine = engine.copy();
        if (brake != null) copy.brake = brake.copy();
        if (wheel != null) copy.wheel = wheel.copy();
        if (body != null) copy.body = body.copy();
        if (exhaust != null) copy.exhaust = exhaust.copy();

        return copy;
    }
}
