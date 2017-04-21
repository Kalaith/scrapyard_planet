using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    void Update() {

        movePlayer();

    }

    void movePlayer() {
        if (Input.GetMouseButton(0)) {
            PlayerController p = (PlayerController)FindObjectOfType(typeof(PlayerController));
            p.PlayerDestination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
