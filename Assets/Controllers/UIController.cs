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
                //Camera.main.rect = new Rect(0, 0, 1, 1);
                Debug.Log("Game Over.");
                gameOver.enabled = true;
                StartCoroutine(TitleScreenDisplay());
            }
        }
	}

    IEnumerator TitleScreenDisplay() {
        Debug.Log("Going to wait 5 seconds then show the title screen.");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("TitleScreen");
    }

}
