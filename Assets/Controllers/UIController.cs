using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour {


    public Text powerValue;
    public Text materialValue;
    public Text coreValue;
    public Image gameOver;

    GameController gc;

    // Use this for initialization
    void Start () {
		gc = (GameController)FindObjectOfType(typeof(GameController));
        gameOver.enabled = false;
    }

    // Update is called once per frame
    void Update() {

        if (gc != null) {
            powerValue.text = (gc.OperationalPowerUsage + gc.ReservedPowerUsage).ToString();
            materialValue.text = gc.Materials.ToString();
            coreValue.text = gc.Core.ToString();

            if (gc.GameOver) {

                gameOver.enabled = true;
                StartCoroutine(TitleScreenDisplay());
            }
        }
	}

    IEnumerator TitleScreenDisplay() {

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("TitleScreen");
    }

}
