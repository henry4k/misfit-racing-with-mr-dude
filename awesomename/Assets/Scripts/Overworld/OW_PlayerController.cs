using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_PlayerController : MonoBehaviour {
    public float speed = 1;
    public float talkingDistance = 1;
    public Rigidbody2D body;
    public Main main;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize();
        body.AddForce(movement * speed);
  
        if(Input.GetKeyUp(KeyCode.E)) {
            foreach(NpcReference npcRef in main.references.npcReferences) {
                Debug.Log(Vector3.Distance(npcRef.transform.position, transform.position));
                if (Vector3.Distance(npcRef.transform.position, transform.position) < talkingDistance) {
                    npcRef.npc.activateDialog();
                    break; // break because multiple open dialogs are disliked
                }
            }
        }
    }
}
