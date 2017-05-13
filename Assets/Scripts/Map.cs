using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map {

    Tile[,] tiles;
    int width;
    int height;
    private Path_TileGraph tileGraph;

    public Path_TileGraph TileGraph {
        get {
            return tileGraph;
        }

        set {
            tileGraph = value;
        }
    }


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

    public Map(string fileName, int width = 11, int height = 11) {
        this.width = width;
        this.height = height;

        CreateMapFromFile(fileName, width, height);
        //CreateMap();

        Debug.Log("World created with " + (width * height) + " tiles");

    }

    public void CreateMap() {
        tiles = new Tile[width, height];

        Tile.TileType tileType;
        int cost = 1;
  
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1 || (y == 5 && x < 4)) {
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

        TileGraph = new Path_TileGraph(this);

    }

    // Quick and temporary map solution
    public void CreateMapFromFile(string filename, int width, int height) {
        tiles = new Tile[width, height];

        string line;
        string[] split;

        // Read the file
        System.IO.StreamReader file = new System.IO.StreamReader(filename);
        // we reverse this, otherwise the map is upside down compared to the file.
        for (int y = height-1; y >= 0; y--) {
            if ((line = file.ReadLine()) != null) {
                split = line.Split(' ');
                for (int x = 0; x < width; x++) {
                    // Empty
                    if (split[x] == "0") {
                        tiles[x, y] = new Tile(this, x, y, 0);
                        tiles[x, y].Type = Tile.TileType.Empty;
                    }
                    // Wall
                    if (split[x] == "1") {
                        tiles[x, y] = new Tile(this, x, y, 0);
                        tiles[x, y].Type = Tile.TileType.Wall;
                    }
                    // Floor
                    if (split[x] == "2") {
                        tiles[x, y] = new Tile(this, x, y, 1);
                        tiles[x, y].Type = Tile.TileType.Floor;
                    }
                    // Dirt
                    if (split[x] == "3") {
                        tiles[x, y] = new Tile(this, x, y, 3);
                        tiles[x, y].Type = Tile.TileType.Dirt;
                    }
                    // Grass
                    if (split[x] == "4") {
                        tiles[x, y] = new Tile(this, x, y, 4);
                        tiles[x, y].Type = Tile.TileType.Grass;
                    }
                    // 3 is a floor with an interactive item
                    if (split[x] == "9") {
                        tiles[x, y] = new Tile(this, x, y, 1);
                        tiles[x, y].Type = Tile.TileType.Floor;
                        tiles[x, y].Item = new InteractiveItem(tiles[x, y]);
                    }
                }  
            }
        }

        file.Close();
        TileGraph = new Path_TileGraph(this);

    }

    public Tile GetTileAt(int x, int y) {
        if(x >= width || x < 0 || y >= height || y < 0) {
            //Debug.LogError("Tile ("+x+","+y+"), is out of range");
            return null;
        }

        if (tiles[x, y] == null) {
            tiles[x, y] = new Tile(this, x, y, 1);
        }

        return tiles[x, y];
    }



}
