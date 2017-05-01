using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    MapController mc;

    // Use this for initialization
    void Start () {
        mc = (MapController)FindObjectOfType(typeof(MapController));
    }

    // Update is called once per frame
    void Update() {

        movePlayer();

    }

    void movePlayer() {
        if (Input.GetMouseButton(0)) {
            PlayerController p = (PlayerController)FindObjectOfType(typeof(PlayerController));
            Vector3 moveTo = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log("Player attempts to move to "+ Mathf.FloorToInt(moveTo.x) +":"+ Mathf.FloorToInt(moveTo.y));
            if (mc.Map.GetTileAt(Mathf.FloorToInt(moveTo.x), Mathf.FloorToInt(moveTo.y)) != null) {

                p.Player.DestTile = mc.Map.GetTileAt(Mathf.FloorToInt(moveTo.x), Mathf.FloorToInt(moveTo.y));
            }
        }
    }
}
