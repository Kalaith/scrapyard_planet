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
        this.Speed = speed;
    }

    public Vector3 getPosition() {
        return new Vector3(x, y, 0);
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
