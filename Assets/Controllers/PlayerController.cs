using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Vector3 currentFramePosition;
    Vector3 lastFramePosition;

    Player player;
    GameObject player_go;

    public Sprite playerSprite;

    // Use this for initialization
    void Start () {
        player = new Player("Bob", 0, 0, 1.5f);

       
        player_go = new GameObject();

        player_go.AddComponent<SpriteRenderer>();
        player_go.GetComponent<SpriteRenderer>().sprite = playerSprite;
        player_go.GetComponent<SpriteRenderer>().sortingOrder = 1;



    }

    // Update is called once per frame
    void Update () {
        currentFramePosition = player.getPosition();

        playerMovement();

        lastFramePosition = player.getPosition();
    }

    public void playerMovement() {
        if (Input.GetKey(KeyCode.A))
            currentFramePosition.x--;

        if (Input.GetKey(KeyCode.W))
            currentFramePosition.y++;

        if (Input.GetKey(KeyCode.S))
            currentFramePosition.y--;

        if (Input.GetKey(KeyCode.D))
            currentFramePosition.x++;

        player_go.transform.position += currentFramePosition * player.Speed * Time.deltaTime;

    }

}

