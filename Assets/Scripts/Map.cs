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
        int cost = 1;
        Tile.TileType tileType;
        tiles = new Tile[width, height];
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if(x==0 || x == width-1 || y == 0 || y == height-1) {
                    cost = 0;
                    tileType = Tile.TileType.Wall;
                } else {
                    cost = 1;
                    tileType = Tile.TileType.Floor;
                }
                tiles[x, y] = new Tile(this, x, y, cost);
                tiles[x, y].Type = tileType;
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
            tiles[x, y] = new Tile(this, x, y, 1);
        }

        return tiles[x, y];
    }



}
