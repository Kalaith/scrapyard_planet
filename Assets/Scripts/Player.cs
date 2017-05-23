using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class Player {

    public bool OnSwitch;


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

    public Player(float speed, Map map, Tile currTile) {
        this.speed = speed;
        this.currTile = currTile;
        this.destTile = currTile;
        this.nextTile = currTile;

        this.map = map;
    }

    public void Update_HandleMovement(float deltaTime) {

        if(currTile.Item != null) {
            OnSwitch = true;
        } else {
            OnSwitch = false;
        }

        if (currTile == DestTile) {
            pathAStar = null;
            return;
        }

        // is nextTile null or the current tile
        if(nextTile == null || nextTile == currTile) {
            // Has pathAStar found a valid path
            if (pathAStar == null || pathAStar.Length() == 0) {
                // Generate a new path, needs the map, the current tile and the destination tile.
                pathAStar = new Path_AStar(map, currTile, destTile);

                // pathAStar could not find a valid path.
                if(pathAStar.Length() == 0) {
                    //Debug.Log("Path_AStar returned no path to destination");
                    pathAStar = null;
                    return;
                }
            }

            // we have a path, set nextTile to the next tile along the path.
            nextTile = pathAStar.Dequeue();

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
            pathAStar = null;
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
