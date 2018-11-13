﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkshopManager : MonoBehaviour {
    public Main main;

    KeyCode navForward;
    KeyCode navBackward;
    KeyCode categoryForward;
    KeyCode categoryBackward;
    KeyCode enter;
    private GameObject partShowCase;
    public GameObject categorySelection;
    public GameObject carStats;
    public GameObject tooltip;
    public GameObject navControls;
    [Space(10)]
    public GameObject partShowCasePrefab;
    public GameObject carStatsBarPrefab;
    public GameObject carStatsBarHighlightedPrefab;
    public GameObject carStatsBarHighlightedNegativePrefab;
    public GameObject canvas;
    [HideInInspector]
    public string text_tooltip;
    [HideInInspector]
    public string text_bought = "Install";
    [HideInInspector]
    public string text_boughtAndEquiped = "is already Installed.";
    [HideInInspector]
    public string text_costs = "costs";
    [HideInInspector]
    public string text_buy = "Buy";
    [HideInInspector]
    public string text_install = "Install";

    private GameObject buyButton;
    List<Part> parts = new List<Part>();
    int index = 0;
    int categoryIndex = 0;

    private Car oldCar;
	void Start () {
        navForward = main.settings.D;
        navBackward = main.settings.A;
        categoryForward = main.settings.S;
        categoryBackward = main.settings.W;
        enter = main.settings.Enter;
        buyButton = navControls.transform.GetChild(2).gameObject;
        parts = getParts();
        createPartShowCase();
        highlightCategory();
        oldCar = main.references.playerReference.player.car.copy();
        calculateAttributes(oldCar);
        createStatsPreview();
    }

    void Update() {
        if (Input.GetKeyUp(navForward)) {
            Destroy(partShowCase);
            index++;
            createPartShowCase();
            createStatsPreview();
        }
        if (Input.GetKeyUp(navBackward)) {
            Destroy(partShowCase);
            index--;
            createPartShowCase();
            createStatsPreview();
        }
        if (Input.GetKeyUp(categoryForward)) {
            categoryIndex++;
            parts = getParts();
            index = 0;
            Destroy(partShowCase);
            createPartShowCase();
            highlightCategory();
            createStatsPreview();
        }
        if (Input.GetKeyUp(categoryBackward)) {
            categoryIndex--;
            parts = getParts();
            index = 0;
            Destroy(partShowCase);
            createPartShowCase();
            highlightCategory();
            createStatsPreview();
        }
        if (Input.GetKeyUp(enter)) {
            int partIndex = Utils.mod(index, parts.Count);
            Part p = parts[partIndex];
            bool isOwned = main.references.playerReference.player.partsOwned.Contains(p);
            bool isEquiped = checkIsEquiped(p);
            if (!isOwned) {
                buyPart(p);
            } else {
                if (!isEquiped) {
                    installPart(main.references.playerReference.player.car, p);
                    oldCar = main.references.playerReference.player.car;
                }
            }
            Destroy(partShowCase);
            createPartShowCase();
            createStatsPreview();
        }
    }

    private void buyPart(Part p) {
        Player player = main.references.playerReference.player;
        if (player.money >= p.value) {
            player.money -= p.value;
            player.partsOwned.Add(p);
        }
        
    }

    private void highlightCategory() {
        GameObject categories = categorySelection.transform.GetChild(0).gameObject;
        for(int i = 0; i< categories.transform.childCount; i++) {
            GameObject child = categories.transform.GetChild(i).gameObject;
            child.transform.GetChild(0).gameObject.SetActive(false);
            child.transform.GetChild(2).gameObject.SetActive(false);
        }
        GameObject current = categories.transform.GetChild(Utils.mod(categoryIndex, 5)).gameObject;
        current.transform.GetChild(0).gameObject.SetActive(true);
        current.transform.GetChild(2).gameObject.SetActive(true);
    }

    private void destroyAllChildren(GameObject parent) {
        int childs = parent.transform.childCount;
        for (int i = 0; i < childs; i++) {
            GameObject.Destroy(parent.transform.GetChild(i).gameObject);
        }
    }

    private void createStatsPreview() {
        Car previewCar = oldCar.copy();
        if (parts != null && parts.Count >= 1) {
            Part p = parts[Utils.mod(index, parts.Count)];
            if (p != null) {
                installPart(previewCar, p);
            }
        }
       
        GameObject accelerationContainer = getCarStatsAttributeBarContainer("Acceleration");
        GameObject maxSpeedContainer = getCarStatsAttributeBarContainer("MaxSpeed");
        GameObject brakingPowerContainer = getCarStatsAttributeBarContainer("BreakingPower");
        // remove all existing bars
        destroyAllChildren(accelerationContainer);
        destroyAllChildren(maxSpeedContainer);
        destroyAllChildren(brakingPowerContainer);

        int accDif = previewCar.acceleration - oldCar.acceleration;
        int mspdDif = previewCar.maxSpeed - oldCar.maxSpeed;
        int brkpDif = previewCar.brakingPower - oldCar.brakingPower;
        int offset = 0;
        if (accDif < 0) offset = accDif;

        for (int i = 0; i< oldCar.acceleration + offset; i++) {
            GameObject bar = Instantiate(carStatsBarPrefab);
            bar.transform.SetParent(accelerationContainer.transform);
            bar.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        offset = 0;
        if (mspdDif < 0) offset = mspdDif;
        for (int i = 0; i < oldCar.maxSpeed + offset; i++) {
            GameObject bar = Instantiate(carStatsBarPrefab);
            bar.transform.SetParent(maxSpeedContainer.transform);
            bar.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        offset = 0;
        if (brkpDif < 0) offset = brkpDif;
        for (int i = 0; i < oldCar.brakingPower + offset; i++) {
            GameObject bar = Instantiate(carStatsBarPrefab);
            bar.transform.SetParent(brakingPowerContainer.transform);
            bar.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }

        createBarHighlights(accelerationContainer, accDif);
        createBarHighlights(maxSpeedContainer, mspdDif);
        createBarHighlights(brakingPowerContainer, brkpDif);
    }

    private void createBarHighlights(GameObject barContainer, int dif) {
        if(dif > 0) {
            for (int i = 0; i < dif; i++) {
                GameObject bar = Instantiate(carStatsBarHighlightedPrefab);
                bar.transform.SetParent(barContainer.transform);
                bar.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
        } else if(dif < 0){
            for(int i = 0; i < -dif; i++) {
                GameObject bar = Instantiate(carStatsBarHighlightedNegativePrefab);
                bar.transform.SetParent(barContainer.transform);
                bar.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void createPartShowCase() {
        if(parts != null && parts.Count > 0) {
            int partIndex = Utils.mod(index, parts.Count);
            Part p = parts[partIndex];
            bool isOwned = main.references.playerReference.player.partsOwned.Contains(p);
            GameObject showCase = Instantiate(partShowCasePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Image partImage = showCase.GetComponent<Image>();
            Image partOverlay = showCase.transform.GetChild(1).GetComponent<Image>();
            Text t = showCase.transform.GetChild(0).GetComponent<Text>();
            Text number = showCase.transform.GetChild(2).GetComponent<Text>();
            Text buyButtonText = buyButton.transform.GetChild(0).GetComponent<Text>();
            number.text = (partIndex +1) + "/" + parts.Count;
            partImage.sprite = Resources.Load("Textures/" + p.spriteName, typeof(Sprite)) as Sprite;
            buyButton.SetActive(false);
            if (isOwned) {
                bool isEquiped = checkIsEquiped(p);
                if (isEquiped) {
                    t.text = p.name + " " + text_boughtAndEquiped;
                }
                else {
                    t.text = text_bought + " " + p.name + "?";
                    buyButton.SetActive(true);
                    buyButtonText.text = text_install;
                }
                partOverlay.enabled = false;
            }
            else {
                t.text = p.name + " " + text_costs + " " + p.value;
                buyButton.SetActive(true);
                buyButtonText.text = text_buy;
                partOverlay.enabled = true;
            }
            showCase.transform.SetParent(canvas.transform);
            RectTransform rt = showCase.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            this.partShowCase = showCase;
        }
    }

    private List<Part> getParts() {
        string category = getSelectedCategory();
        return main.getGame().parts[category];
    }

    private string getSelectedCategory() {
        int category = Utils.mod(categoryIndex, 5);
        return main.getGame().categories[category];
    }

   private bool checkIsEquiped(Part p) {
        Player player = main.references.playerReference.player;
        if(player.car.engine != null && player.car.engine.id.Equals(p.id)) {
            return true;
        }
        if (player.car.brake != null && player.car.brake.id.Equals(p.id)) {
            return true;
        }
        if (player.car.wheel != null && player.car.wheel.id.Equals(p.id)) {
            return true;

        }
        if (player.car.body != null && player.car.body.id.Equals(p.id)) {
            return true;
        }
        if (player.car.exhaust != null && player.car.exhaust.id.Equals(p.id)) {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Installs the part to the specific slot and recalculates the Attributes afterwards
    /// </summary>
    /// <param name="car">Car Object to install the Part into.</param>
    /// <param name="p">The new part to install.</param>
    private void installPart(Car car, Part p) {
        if("Engine".Equals(p.category)) {
            car.engine = p;
        }
        else if ("Brake".Equals(p.category)) {
            car.brake = p;
        }
        else if("Wheel".Equals(p.category)) {
            car.wheel = p;
        }
        else if("Body".Equals(p.category)) {
            car.body = p;
        }
        else if("Exhaust".Equals(p.category)) {
            car.exhaust = p;
        }
        calculateAttributes(car);
    }

    /// <summary>
    /// Recalculates the car attributes(acceleration, maxSpeed..) from the (changed) car parts.
    /// </summary>
    /// <param name="car"></param>
    private void calculateAttributes(Car car) {
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
        foreach(Part p in carParts) {
            if(p != null) {
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

    private GameObject getCarStatsAttributeBarContainer(string attribute) {
        for(int i = 0; i < carStats.transform.childCount; i++) {
            GameObject child = carStats.transform.GetChild(i).gameObject;
            if(child.name.Equals(attribute)) {
                return child.transform.GetChild(1).gameObject;
            }
        }
        return null;
    }
}
