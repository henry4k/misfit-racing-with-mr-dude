using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryEntry : MonoBehaviour {
    public WorkshopManager workshopManager;
	public int index;

    public void OnClick() {
        workshopManager.selectCategory(index);
    }
}
