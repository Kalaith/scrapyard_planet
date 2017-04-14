using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour {

    public Sprite floorSprite;

    Map map;

	// Use this for initialization
	void Start () {
        map = new Map();

        for (int x = 0; x < map.Width; x++) {
            for (int y = 0; y < map.Height; y++) {
                Tile tile_data = map.GetTileAt(x, y);
                GameObject tile_go = new GameObject();

                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);
                tile_go.transform.SetParent(this.transform, true);
                tile_go.AddComponent<SpriteRenderer>();
                tile_data.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChanged(tile, tile_go); });
                tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
                tile_go.GetComponent<SpriteRenderer>().sortingOrder = 0;

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
