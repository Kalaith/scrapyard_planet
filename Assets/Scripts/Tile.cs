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

    public Tile(Map map, int x, int y) {
        this.map = map;
        this.x = x;
        this.y = y;
    }
}
