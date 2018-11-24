using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {
    public Main main;

    public void toWorkshop() {
        main.getGame().toWorkshop();
    }
	
}
