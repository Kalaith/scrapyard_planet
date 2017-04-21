using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player {

    string name;
    float x;
    float y;
    float speed;


    public Player(string name, float x, float y, float speed) {
        this.name = name;
        this.x = x;
        this.y = y;
        this.speed = speed;
    }

    public Vector3 getPosition() {
        return new Vector3(x, y);
    }

    public void setPosition(Vector3 newPosition) {
        x = newPosition.x;
        y = newPosition.y;
    }

    public float Speed {
        get { return speed; }
        set { speed = value; }
    }

}
