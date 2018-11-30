using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OW_PlayerController : MonoBehaviour {
    private float speed = 1;
    private float talkingDistance = 1;
    public Rigidbody2D body;
    public Main main;
    public GameObject controllerToggle;
    public Animator animator;
	// Use this for initialization
	void Start () {
        speed = main.settings.playerMovementSpeed;
        talkingDistance = main.settings.playerNpcTalkingDistance;
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void FixedUpdate() {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        animator.SetFloat("Horizontal", moveHorizontal);
        animator.SetFloat("Vertical", moveVertical);
        // move player or stop him immidiatly
        if (moveHorizontal == 0 && moveVertical == 0) {
            body.velocity = Vector3.zero;
        } else {
            movement.Normalize();
            body.velocity = movement * speed;
        }

        if (Input.GetKeyUp(main.settings.Enter)) {
            foreach (NpcReference npcRef in main.references.npcReferences) {
                if (Vector3.Distance(npcRef.transform.position, transform.position) < talkingDistance) {
                    npcRef.npc.activateDialog();
                    controllerToggle.SetActive(false);
                    break; // break because multiple open dialogs are disliked
                }
            }
        }
    }
}
