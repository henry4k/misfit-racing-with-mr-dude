using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
    public static bool isGameLoaded = false;
    public static bool isNewGameStart = false;
    public static SaveGameObject currentSave = null; //inbetween scene save
    public static Enemy currentEnemy = null;

    public static int mod(int x, int m) {
        if (m == 0) return 0;
        return (x % m + m) % m;
    }

    /// <summary>
    /// Recalculates the car attributes(acceleration, maxSpeed..) from the (changed) car parts.
    /// </summary>
    /// <param name="car"></param>
    public static void calculateAttributes(Car car) {
        List<Part> carParts = new List<Part>();
        carParts.Add(car.engine);
        carParts.Add(car.brake);
        carParts.Add(car.wheel);
        carParts.Add(car.body);
        carParts.Add(car.exhaust);
        car.acceleration = 0;
        car.maxSpeed = 0;
        car.brakingPower = 0;
        car.hp = 0;
        foreach (Part p in carParts) {
            if (p != null) {
                foreach (Attribute attribute in p.attributes) {
                    if ("Acceleration".Equals(attribute.category)) {
                        car.acceleration += attribute.value;
                    }
                    if ("MaxSpeed".Equals(attribute.category)) {
                        car.maxSpeed += attribute.value;
                    }
                    if ("BreakingPower".Equals(attribute.category)) {
                        car.brakingPower += attribute.value;
                    }
                }
            }
        }
    }
}
