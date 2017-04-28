using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Sprite playerSprite;

    Player player;
    GameObject player_go;
    MapController mc;

    public Player Player {
        get {
            return player;
        }

        set {
            player = value;
        }
    }

    // Use this for initialization
    void Start () {
        mc = (MapController)FindObjectOfType(typeof(MapController));

        // Create a new player
        Player = new Player("Bob", 2f, mc.Map, mc.Map.GetTileAt(2, 2));
        
        // Instaiate a game object for the player
        player_go = new GameObject();

        // Add a SpriteRender and assign a sprite
        player_go.AddComponent<SpriteRenderer>();
        player_go.GetComponent<SpriteRenderer>().sprite = playerSprite;
        player_go.GetComponent<SpriteRenderer>().sortingOrder = 1;

        
    }


    // Update is called once per frame
    void Update () {

        Player.Update_HandleMovement(Time.deltaTime);
        player_go.transform.position = new Vector3(Player.X, Player.Y, 0);
        Camera.main.transform.position = new Vector3(Player.X, Player.Y, Camera.main.transform.position.z);

    }
}

