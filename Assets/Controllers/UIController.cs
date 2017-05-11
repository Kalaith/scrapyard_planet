using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {


    public Text powerValue;
    public Text materialValue;
    public Text gameOver;


    GameController gc;

    // Use this for initialization
    void Start () {
		gc = (GameController)FindObjectOfType(typeof(GameController));
        gameOver.enabled = false;
    }

    // Update is called once per frame
    void Update () {

        powerValue.text = gc.PowerUsage.ToString();
        materialValue.text = gc.Materials.ToString();

        if (gc.GameOver) {
            gameOver.enabled = true;
        }
	}
}
