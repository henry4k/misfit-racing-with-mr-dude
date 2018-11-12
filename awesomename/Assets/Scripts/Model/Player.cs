using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    public Car car;
    public int money;
    public int score;
    public string name;

    public List<Part> partsOwned = new List<Part>();

    public Player() {

    }

    public Player(string name, Car car, int money, int score) {
        this.name = name;
        this.car = car;
        this.money = money;
        this.score = score;
    }
}
