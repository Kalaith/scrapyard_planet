using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile {

    public enum TileType { Empty, Floor, Wall };

    TileType type = TileType.Empty;

    Map map;
    int x;
    int y;
    int movementCost; // cost to move through the tile

    Action<Tile> cbTileTypeChanged;

    public TileType Type {
        get {
            return type;
        }

        set {
            type = value;
            if (cbTileTypeChanged != null)
                cbTileTypeChanged(this);
            
        }
    }

    public int Cost {
        get {
            return movementCost;
        }
    }

    public int X {
        get {
            return x;
        }
    }

    public int Y {
        get {
            return y;
        }
    }
    
    public void RegisterTileTypeChangedCallback(Action<Tile> callback) {
        cbTileTypeChanged += callback;
    }

    public void UnregisterTileTypeChangedCallback(Action<Tile> callback) {
        cbTileTypeChanged -= callback;
    }

    public Tile(Map map, int x, int y, int cost) {
        this.map = map;
        this.x = x;
        this.y = y;
        this.movementCost = cost;
    }

    public bool isNeighbour(Tile tile, bool diagOkay = false) {
        return Mathf.Abs(this.X - tile.X) + Mathf.Abs(this.Y - tile.Y) == 1 || 
            (diagOkay && (Mathf.Abs(this.X - tile.X) == 1 && Mathf.Abs(this.Y - tile.Y) == 1));
    }

    public Tile[] GetNeighbours(bool diagOkay = false) {

        Tile[] ns;

        if (diagOkay == false) {
            ns = new Tile[4];
        } else {
            ns = new Tile[8];
        }

        Tile n;
        n = map.GetTileAt(x, y + 1);
        ns[0] = n;
        n = map.GetTileAt(x + 1, y);
        ns[1] = n;
        n = map.GetTileAt(x, y - 1);
        ns[2] = n;
        n = map.GetTileAt(x - 1, y);
        ns[3] = n;

        if (diagOkay == true) {
            n = map.GetTileAt(x + 1, y + 1);
            ns[4] = n;
            n = map.GetTileAt(x + 1, y - 1);
            ns[5] = n;
            n = map.GetTileAt(x - 1, y - 1);
            ns[6] = n;
            n = map.GetTileAt(x - 1, y + 1);
            ns[7] = n;
        }

        return ns;
    }
}
