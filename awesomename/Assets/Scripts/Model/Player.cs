using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    private Car car;
    private int money;
    private int score;
    private string name;

    public Player() {

    }

    public Player(string name, Car car, int money, int score) {
        this.name = name;
        this.car = car;
        this.money = money;
        this.score = score;
    }
    public Car getCar() { return car; }
    public void setCar(Car car) { this.car = car; }

    public int getMoney() { return money; }
    public void setMoney(int money) { this.money = money; }

    public int getScore() { return score; }
    public void setScore(int score) { this.score = score; }

    public string getName() { return name; }
    public void setName(string name) { this.name = name; }
}
