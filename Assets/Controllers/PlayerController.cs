using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Player player;
    GameObject player_go;

    public Sprite playerSprite;

    // Use this for initialization
    void Start () {
        player = new Player("Bob", 5, 5, 1.5f);
        
        player_go = new GameObject();


        player_go.AddComponent<SpriteRenderer>();
        player_go.GetComponent<SpriteRenderer>().sprite = playerSprite;
        player_go.GetComponent<SpriteRenderer>().sortingOrder = 1;

        player_go.transform.position = player.getPosition();
    }

    // Update is called once per frame
    void Update () {

        playerMovement();
  
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
        Camera.main.transform.position = new Vector3(player_go.transform.position.x, player_go.transform.position.y, Camera.main.transform.position.z);

    }

}

