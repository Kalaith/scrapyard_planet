using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    public static MapController Instance { get; protected set; }
    public Sprite floorSprite;
    public Sprite wallSprite;
    public Sprite itemSwitchSprite;

    public Map Map { get; protected set; }

    InteractiveController itemController;

    // Use this for initialization
    void Start () {
        Instance = this;
        Map = new Map();
        itemController = (InteractiveController)FindObjectOfType(typeof(InteractiveController));

        for (int x = 0; x < Map.Width; x++) {
            for (int y = 0; y < Map.Height; y++) {
                Debug.Log("X:"+x+"Y:"+y);
                Tile tile_data = Map.GetTileAt(x, y);
                GameObject tile_go = new GameObject();

                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 1);
                tile_go.transform.SetParent(this.transform, true);
                tile_go.AddComponent<SpriteRenderer>();
                tile_data.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChanged(tile, tile_go); });
                if (tile_data.Type == Tile.TileType.Floor) {
                    tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
                } else if(tile_data.Type == Tile.TileType.Wall) {
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                }
                tile_go.GetComponent<SpriteRenderer>().sortingOrder = 0;

                if(tile_data.Item != null) {
                    Debug.Log("We have an item to spawn a game object on this tile at X:"+x+"Y:"+y);
                    itemController.addItem(tile_data);
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
