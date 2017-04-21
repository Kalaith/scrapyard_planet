using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Sprite playerSprite;

    Player player;
    GameObject player_go;

    private Vector3 playerDestination;

    public Vector3 PlayerDestination {
        get { return playerDestination; }
        set { playerDestination = value; }
    }

    // Use this for initialization
    void Start () {
        // Create a new player
        player = new Player("Bob", 5, 5, 3f);
        
        // Instaiate a game object for the player
        player_go = new GameObject();

        // Add a SpriteRender and assign a sprite
        player_go.AddComponent<SpriteRenderer>();
        player_go.GetComponent<SpriteRenderer>().sprite = playerSprite;
        player_go.GetComponent<SpriteRenderer>().sortingOrder = 1;

        // Set the players sprite to the players known position
        player_go.transform.position = player.getPosition();
        playerDestination = player.getPosition();


    }

    // Update is called once per frame
    void Update () {
        // Update the players location
        playerMovement();

        // Move towards the playerDestination
        if (PlayerDestination != player.getPosition()) {

            // Whats with the 10...
            Debug.Log(Vector3.Distance(player.getPosition(), playerDestination) - 10);
            //Debug.Log("Move Towards: " + Vector3.MoveTowards(player.getPosition(), playerDestination, (player.Speed * Time.deltaTime)));
            player_go.transform.position = Vector3.MoveTowards(player.getPosition(), playerDestination, (player.Speed * Time.deltaTime));

            player.setPosition(player_go.transform.position);
            //}
        }


        // Have the camera stay on the players position, apart from the Z axis.
        Camera.main.transform.position = new Vector3(player.getPosition().x, player.getPosition().y, Camera.main.transform.position.z);
    }

    public void playerMovement() {
        Vector3 movementDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.A))
            movementDirection.x--;

        if (Input.GetKey(KeyCode.W))
            movementDirection.y++;

        if (Input.GetKey(KeyCode.S))
            movementDirection.y--;

        if (Input.GetKey(KeyCode.D))
            movementDirection.x++;

        player_go.transform.position += movementDirection * player.Speed * Time.deltaTime;
        player.setPosition(player_go.transform.position);

        Camera.main.transform.position = new Vector3(player.getPosition().x, player.getPosition().y, Camera.main.transform.position.z);
    }

}

