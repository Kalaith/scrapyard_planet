using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player {

    string name;

    public float X {
        get {
            return Mathf.Lerp(currTile.X, nextTile.X, movementPerc);
        }
    }

    public float Y {
        get {
            return Mathf.Lerp(currTile.Y, nextTile.Y, movementPerc);
        }
    }

    float movementPerc;
    float speed;

    Path_AStar pathAStar;
    Tile currTile;
    Tile nextTile;
    Tile destTile;
    Map map;

    public Player(string name, float speed, Map map, Tile currTile) {
        this.name = name;
        this.speed = speed;
        this.currTile = currTile;
        this.destTile = currTile;
        this.nextTile = currTile;

        this.map = map;
    }

    public void Update_HandleMovement(float deltaTime) {

        if (currTile == DestTile) {
            return;
        }

        if(nextTile == null || nextTile == currTile) {
            // Ask the pathfinding for the next tile
            if (pathAStar == null || pathAStar.Length() == 0) {
                pathAStar = new Path_AStar(map, currTile, destTile);

                if(pathAStar.Length() == 0) {
                    Debug.LogError("Path_AStar returned no path to destination");
                    pathAStar = null;
                    return;
                }
            }

            nextTile = pathAStar.Dequeue();
            
            if(nextTile == currTile) {
                Debug.LogError("nextTime is currTile?");
            }
        }

        float distToTravel = Mathf.Sqrt(
            Mathf.Pow(currTile.X - nextTile.X, 2) +
            Mathf.Pow(currTile.Y - nextTile.Y, 2));

        float distThisFrame = Speed * deltaTime;
        float percThisFrame = distThisFrame / distToTravel;

        MovementPerc += percThisFrame;

        if (MovementPerc >= 1) {
            currTile = nextTile;
            MovementPerc = 0;
        }
    }

    public float Speed {
        get { return speed; }
        set { speed = value; }
    }
    public float MovementPerc {
        get { return movementPerc; }
        set { movementPerc = value; }
    }

    public Tile DestTile {
        get {
            return destTile;
        }

        set {
            destTile = value;
        }
    }

    public Tile CurrTile {
        get {
            return currTile;
        }

        set {
            currTile = value;
        }
    }
}
