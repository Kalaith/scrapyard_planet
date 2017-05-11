using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    public static MapController Instance { get; protected set; }
    public Sprite floorSprite;
    public Sprite wallSprite;
    public Sprite itemSwitchSprite;

    public Sprite grassSprite;
    public Sprite dirtSprite;

    public Map Map { get; protected set; }
    public Map ExternalMap { get; protected set; }

    InteractiveController itemController;

    // Use this for initialization
    void Start () {
        Instance = this;
        Map = new Map("Assets/Levels/LEVEL_1.txt", 11, 11);
        ExternalMap = new Map("Assets/Levels/LEVEL_ENEMY_1.txt", 10, 10);

        itemController = (InteractiveController)FindObjectOfType(typeof(InteractiveController));

        for (int x = 0; x < Map.Width; x++) {
            for (int y = 0; y < Map.Height; y++) {
                Tile tile_data = Map.GetTileAt(x, y);
                GameObject tile_go = new GameObject();

                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);
                tile_go.transform.SetParent(this.transform, true);
                tile_go.AddComponent<SpriteRenderer>();
                tile_data.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChanged(tile, tile_go); });
                 if (tile_data.Type == Tile.TileType.Floor) {
                    tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
                } else if (tile_data.Type == Tile.TileType.Wall) {
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                }

                tile_go.GetComponent<SpriteRenderer>().sortingOrder = 0;

                if(tile_data.Item != null && itemController != null) {
                    Debug.Log("We have an item to spawn a game object on this tile at X:"+x+"Y:"+y);
                    itemController.addItem(x, y);
                }

            }
        }

        
        int startx = Map.Width+1;
        Debug.Log("Enemy Map Spawning");
        // External Map, next to the players, on right hand side.
        for (int x = 0+startx; x < ExternalMap.Width+startx; x++) {
            for (int y = 0; y < ExternalMap.Height; y++) {
                Debug.Log("Check X:" + (x-startx) + " Check Y:" + y);
                Tile tile_data = ExternalMap.GetTileAt(x-startx, y);
                GameObject tile_go = new GameObject();

                tile_go.name = "ETile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(x, y, 0);
                tile_go.transform.SetParent(this.transform, true);
                tile_go.AddComponent<SpriteRenderer>();
                tile_data.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChanged(tile, tile_go); });
                Debug.Log("Tile Type;"+ tile_data.Type);
                if (tile_data.Type == Tile.TileType.Grass) {
                    tile_go.GetComponent<SpriteRenderer>().sprite = grassSprite;
                } else if (tile_data.Type == Tile.TileType.Dirt) {
                    tile_go.GetComponent<SpriteRenderer>().sprite = dirtSprite;
                }
                tile_go.GetComponent<SpriteRenderer>().sortingOrder = 0;

                if (tile_data.Item != null && itemController != null) {
                    Debug.Log("We have an item to spawn a game object on this tile at X:" + x + "Y:" + y);
                    itemController.addItem(x, y);
                }

            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTileTypeChanged(Tile tile_data, GameObject tile_go) {
        if (tile_data.Type == Tile.TileType.Floor) {
            tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
        } else {
            tile_go.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
