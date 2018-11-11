using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car {

    private int hp;
    private int acceleration;
    private int maxSpeed;
    private int brakingPower;

    private Part engine;
    private Part brake;
    private Part wheel;
    private Part body;
    private Part exhaust;

    public int getHp() { return hp; }
    public void setHp(int hp) { this.hp = hp; }

    public int getAcceleration() { return this.acceleration; }
    public void setAcceleration(int acceleration) { this.acceleration = acceleration; }

    public int getMaxSpeed() { return maxSpeed; }
    public void setMaxSpeed(int maxSpeed) { this.maxSpeed = maxSpeed; }

    public int getBrakingPower() { return brakingPower; }
    public void setBrakingPower(int brakingPower) { this.brakingPower = brakingPower; }

    public Part getEngine() { return engine; }
    public void setEngine(Part engine) { this.engine = engine; }

    public Part getBrake() { return brake; }
    public void setBrake(Part brake) { this.brake = brake; }

    public Part getWheel() { return wheel; }
    public void setWheel(Part wheel) { this.wheel = wheel; }

    public Part getBody() { return body; }
    public void setBody(Part body) { this.body = body; }

    public Part getExhaust() { return exhaust; }
    public void setExhaust(Part exhaust) { this.exhaust = exhaust; }
}
