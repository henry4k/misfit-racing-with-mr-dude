using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Rigidbody2D body;
    public Main main;
    Player player;
    public Vector2 velocity;
    public AudioSource engineSound;
    public RaceController racecontroller;

	// Use this for initialization
	void Start () {
        player = main.references.playerReference.player;
        Utils.calculateAttributes(main.references.playerReference.player.car);
        transform.position = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    private void FixedUpdate() {
        body.velocity = Vector3.ClampMagnitude(body.velocity, player.car.maxSpeed);
        
        // pitch change for acceleration
        float p = (-racecontroller.mapBody.velocity.y-racecontroller.maxSpeedOffset) / main.references.playerReference.player.car.maxSpeed;
        engineSound.pitch = 1 + p;
    }

    public void accelerate(float moveVertical) {
        Vector2 movement = new Vector2(0, moveVertical * 0.5f);
        if(movement.y >= 0) velocity = movement * player.car.acceleration;
        else velocity = movement * player.car.brakingPower;
    }

    public void moveToSide(bool right) {
        float movement = 1f;
        if (!right) movement *= -1;
        
        Vector3 pos = transform.position;
        pos.x += movement;
        if (isMoveablePosition(pos))
            transform.position = pos;
    }

   private bool isMoveablePosition(Vector3 pos) {
        bool isMoveable = false;
        isMoveable = boundaryCheck(pos);
        return isMoveable;
    }

    // return true if withinBoundary
    private bool boundaryCheck(Vector3 pos) {
        bool outOfBoundary = true;
        if (pos.x > -1 && pos.x < 6) outOfBoundary = false;        
        return !outOfBoundary;
    }

}
