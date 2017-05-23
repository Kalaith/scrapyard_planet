using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour {

    public static MapController Instance { get; protected set; }
    private Sprite floorSprite;
    private Sprite wallSprite;
    private Sprite wallFloorSprite;
    public Sprite itemSwitchSprite;

    public Sprite grassSprite;
    public Sprite dirtSprite;

    public Map Map { get; protected set; }
    public Map ExternalMap { get; protected set; }

    public int ExternalModifier = 10;
    public int startx = 0;
    private List<InteractiveItem> items;
    private List<InteractiveItem> engines;

    public GameObject Core {
        get {
            return core;
        }

        set {
            core = value;
        }
    }

    private GameObject core;

    InteractiveController itemController;

    // Use this for initialization
    void Start () {
        Instance = this;
        Map = new Map("Assets/Levels/LEVEL_1.txt", 11, 11);
        ExternalMap = new Map("Assets/Levels/LEVEL_ENEMY_1.txt", 30, 30);

        itemController = (InteractiveController)FindObjectOfType(typeof(InteractiveController));

        items = new List<InteractiveItem>();
        engines = new List<InteractiveItem>();

        Debug.Log("Creating Internal Map");
        CreateInternalMap();

        Debug.Log("Creating External Map");
        CreateExternalMap();
    }
	
    private void CreateExternalMap() {
        startx = Map.Width + ExternalModifier;
        Debug.Log("Enemy Map Spawning");
        // External Map, next to the players, on right hand side.
        for (int x = 0 + startx; x < ExternalMap.Width + startx; x++) {
            for (int y = 0; y < ExternalMap.Height; y++) {
                Tile tile_data = ExternalMap.GetTileAt(x - startx, y);
                GameObject tile_go = new GameObject();

                tile_go.name = "ETile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(x, y, 0);
                tile_go.transform.SetParent(this.transform, true);
                tile_go.AddComponent<SpriteRenderer>();
                tile_data.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChanged(tile, tile_go); });

                if (tile_data.Type == Tile.TileType.Grass) {
                    tile_go.GetComponent<SpriteRenderer>().sprite = Resources.Load("ExternalTiles/grass_32px", typeof(Sprite)) as Sprite;
                } else if (tile_data.Type == Tile.TileType.Dirt) {
                    tile_go.GetComponent<SpriteRenderer>().sprite = Resources.Load("ExternalTiles/dirt_32px", typeof(Sprite)) as Sprite;
                } else if (tile_data.Type == Tile.TileType.Roof) {
                    tile_go.GetComponent<SpriteRenderer>().sprite = Resources.Load("InternalShip/metaltile1", typeof(Sprite)) as Sprite;
                } else if (tile_data.Type == Tile.TileType.HasTurrent) {
                    tile_go.GetComponent<SpriteRenderer>().sprite = Resources.Load("InternalShip/metaltile3", typeof(Sprite)) as Sprite;
                } else if (tile_data.Type == Tile.TileType.ExternalWall) {
                    tile_go.GetComponent<SpriteRenderer>().sprite = Resources.Load("InternalShip/metaltile2", typeof(Sprite)) as Sprite;
                } else if (tile_data.Type == Tile.TileType.Core) {
                    tile_go.GetComponent<SpriteRenderer>().sprite = Resources.Load("InternalShip/metaltile4", typeof(Sprite)) as Sprite;
                    Core = tile_go;
                } else if (tile_data.Type == Tile.TileType.Turrent) {
                    tile_go.GetComponent<SpriteRenderer>().sprite = Resources.Load("InternalShip/metaltile5", typeof(Sprite)) as Sprite;
                }

                /*
                5 Roof
                6 HasTurrent
                7 ExternalShipWall
                8 Core
                9 Turrent (Interactive Item)
                */

                tile_go.GetComponent<SpriteRenderer>().sortingOrder = 0;

                if (tile_data.Item != null && itemController != null) {
                    Debug.Log("We have a turrent to spawn on this tile at X:" + x + "Y:" + y);
                    InteractiveItem item = items[0];
                    items.Remove(item);
                    Turret turret = new Turret(item, "Cannon", 3);
                    itemController.addTurret(turret, x, y, Resources.Load("ExternalTiles/gun", typeof(Sprite)) as Sprite);
                }

            }
        }
    }

    private void CreateInternalMap() {
        for (int x = 0; x < Map.Width; x++) {
            for (int y = 0; y < Map.Height; y++) {
                Tile tile_data = Map.GetTileAt(x, y);
                GameObject tile_go = new GameObject();

                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 1);
                tile_go.transform.SetParent(this.transform, true);
                tile_go.AddComponent<SpriteRenderer>();
                tile_data.RegisterTileTypeChangedCallback((tile) => { OnTileTypeChanged(tile, tile_go); });
                if (tile_data.Type == Tile.TileType.Floor) {
                    int randomTile = Random.Range(5, 10);
                    floorSprite = Resources.Load("InternalShip/metaltile" + randomTile, typeof(Sprite)) as Sprite;

                    tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
                } else if (tile_data.Type == Tile.TileType.Wall) {
                    int randomTile = Random.Range(1, 4);
                    wallSprite = Resources.Load("InternalShip/metaltile" + randomTile, typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;

                } else if (tile_data.Type == Tile.TileType.CornerBL) {
                    wallSprite = Resources.Load("InternalShip/walltile4", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.CornerBR) {
                    wallSprite = Resources.Load("InternalShip/walltile4", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    tile_go.GetComponent<SpriteRenderer>().transform.localRotation = Quaternion.Euler(0, 0, 90);
                    tile_go.transform.position = new Vector3(tile_data.X + 1, tile_data.Y, 1);
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.CornerTL) {
                    wallSprite = Resources.Load("InternalShip/walltile4", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    tile_go.GetComponent<SpriteRenderer>().transform.localRotation = Quaternion.Euler(0, 0, -90);
                    tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y + 1, 1);
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.CornerTR) {
                    wallSprite = Resources.Load("InternalShip/walltile4", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    tile_go.GetComponent<SpriteRenderer>().transform.localRotation = Quaternion.Euler(0, 0, 180);
                    tile_go.transform.position = new Vector3(tile_data.X+1, tile_data.Y+1, 1);
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.JoinerL) { 
                    wallSprite = Resources.Load("InternalShip/walltile6", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.JoinerR) { // OUTSTANDING
                    wallSprite = Resources.Load("InternalShip/walltile6", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.JoinerT) { // OUTSTANDING
                    wallSprite = Resources.Load("InternalShip/walltile6", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    tile_go.GetComponent<SpriteRenderer>().transform.localRotation = Quaternion.Euler(0, 0, -90);
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.JoinerB) {
                    wallSprite = Resources.Load("InternalShip/walltile6", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    tile_go.GetComponent<SpriteRenderer>().transform.localRotation = Quaternion.Euler(0, 0, 90);
                    tile_go.transform.position = new Vector3(tile_data.X+1, tile_data.Y, 1);
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.HallwayLR) {
                    wallSprite = Resources.Load("InternalShip/walltile5", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.HallwayTB) {
                    wallSprite = Resources.Load("InternalShip/walltile5", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    tile_go.GetComponent<SpriteRenderer>().transform.localRotation = Quaternion.Euler(0, 0, 90);
                    tile_go.transform.position = new Vector3(tile_data.X + 1, tile_data.Y, 1);
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.DamagedWallB) { // OUTSTANDING
                    wallSprite = Resources.Load("InternalShip/walltiledamaged", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.DamagedWallT) { // OUTSTANDING
                    wallSprite = Resources.Load("InternalShip/walltiledamaged", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.DamagedWallL) { // OUTSTANDING
                    wallSprite = Resources.Load("InternalShip/walltiledamaged", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.DamagedWallR) { // OUTSTANDING
                    wallSprite = Resources.Load("InternalShip/walltiledamaged", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.CrackWallB) { // OUTSTANDING
                    wallSprite = Resources.Load("InternalShip/walltile2", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.CrackWallT) { // OUTSTANDING
                    wallSprite = Resources.Load("InternalShip/walltile2", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.CrackWallL) { // OUTSTANDING
                    wallSprite = Resources.Load("InternalShip/walltile2", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.CrackWallR) { // OUTSTANDING
                    wallSprite = Resources.Load("InternalShip/walltile2", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.WallB) {
                    wallSprite = Resources.Load("InternalShip/walltile1", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.WallT) {
                    wallSprite = Resources.Load("InternalShip/walltile1", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    tile_go.GetComponent<SpriteRenderer>().transform.localRotation = Quaternion.Euler(0, 0, 180);
                    tile_go.transform.position = new Vector3(tile_data.X+1, tile_data.Y+1, 1);
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.WallL) {
                    wallSprite = Resources.Load("InternalShip/walltile1", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    tile_go.GetComponent<SpriteRenderer>().transform.localRotation = Quaternion.Euler(0, 0, -90);
                    tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y+1, 1);
                    CreateTileGO(tile_data);
                } else if (tile_data.Type == Tile.TileType.WallR) {
                    wallSprite = Resources.Load("InternalShip/walltile1", typeof(Sprite)) as Sprite;
                    tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
                    tile_go.GetComponent<SpriteRenderer>().transform.localRotation = Quaternion.Euler(0, 0, 90);
                    tile_go.transform.position = new Vector3(tile_data.X+1, tile_data.Y, 1);
                    CreateTileGO(tile_data);
                }

                tile_go.GetComponent<SpriteRenderer>().sortingOrder = 1;

                if (tile_data.Item != null && itemController != null && tile_data.Item.GetType() != typeof(Engine)) {
                    Debug.Log("We have an item to spawn a game object on this tile at X:" + x + "Y:" + y);
                    items.Add(tile_data.Item);
                    itemController.addItem(tile_data.Item, x, y, Resources.Load("switchbroken", typeof(Sprite)) as Sprite);
                }
                if(tile_data.Item != null && tile_data.Item.GetType() == typeof(Engine)) {
                    engines.Add(tile_data.Item);
                    itemController.addEngine(tile_data.Item, x, y, Resources.Load("switchbroken", typeof(Sprite)) as Sprite);
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

    private void CreateTileGO(Tile tile_data) {
        // Create a floor object
        GameObject wallFloor = new GameObject();
        wallFloor.name = "FloorTile_" + tile_data.X + "_" + tile_data.Y;
        wallFloor.transform.position = new Vector3(tile_data.X, tile_data.Y, 1);
        wallFloor.transform.SetParent(this.transform, true);
        wallFloor.AddComponent<SpriteRenderer>();
        wallFloorSprite = Resources.Load("InternalShip/metaltile1", typeof(Sprite)) as Sprite;
        wallFloor.GetComponent<SpriteRenderer>().sprite = wallFloorSprite;
        wallFloor.GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
}
