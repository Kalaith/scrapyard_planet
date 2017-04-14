using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {

    Tile[,] tiles;
    int width;
    int height;

    public int Width {
        get {
            return width;
        }
    }

    public int Height {
        get {
            return height;
        }
    }

    public Map(int width = 10, int height = 10) {
        this.width = width;
        this.height = height;

        tiles = new Tile[width, height];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                tiles[x, y] = new Tile(this, x, y);
                tiles[x, y].Type = Tile.TileType.Floor;
            }
        }

        Debug.Log("World created with " + (width * height) + " tiles");

    }

    public Tile GetTileAt(int x, int y) {
        if(x > width || x < 0 || y > height || y < 0) {
            Debug.LogError("Tile ("+x+","+y+"), is out of range");
            return null;
        }

        if (tiles[x, y] == null) {
            tiles[x, y] = new Tile(this, x, y);
        }

        return tiles[x, y];
    }



}
