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
    public Image gameWon;

    public Text Countdown;
    public Text Timer;

    public Canvas _ToolTip;

    GameController gc;

    // Use this for initialization
    void Start () {
		gc = (GameController)FindObjectOfType(typeof(GameController));
        gameOver.enabled = false;
        gameWon.enabled = false;

        Countdown.enabled = false;
        Timer.enabled = false;

        _ToolTip.enabled = false;
        
    }

    // Update is called once per frame
    void Update() {

        if (gc != null) {
            powerValue.text = (gc.OperationalPowerUsage + gc.ReservedPowerUsage).ToString();
            materialValue.text = gc.Materials.ToString();
            coreValue.text = gc.Core.ToString();

            if(gc.StartCountDown) {
                Countdown.enabled = true;
                Timer.enabled = true;
                Timer.text = gc.TimeToLaunch.ToString();
            }
            
            if (gc.GameOver && gc.GameWon) {
                gameWon.enabled = true;

                StartCoroutine(TitleScreenDisplay());
            } else if(gc.GameOver && !gc.GameWon) {
                gameOver.enabled = true;
                StartCoroutine(TitleScreenDisplay());
            }
        }
	}

    // We want a item, because we want all key information from it.
    public void ShowToolTip(InteractiveItem item) {

        if (item != null) {
            GameObject.Find("NameValue").GetComponent<Text>().text = item.Name;
            GameObject.Find("StatusValue").GetComponent<Text>().text = item.Status.ToString();
            GameObject.Find("RepairValue").GetComponent<Text>().text = item.RepairProgress.ToString() + " %";
            GameObject.Find("OperationalValue").GetComponent<Text>().text = item.Operational_power_usage.ToString();
            GameObject.Find("ReservedValue").GetComponent<Text>().text = item.Reserved_power_usage.ToString();

            _ToolTip.enabled = true;
        } else {
            _ToolTip.enabled = false;
        }
    }

    public void HideToolTip() {
        _ToolTip.enabled = false;
    }

    IEnumerator TitleScreenDisplay() {

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("TitleScreen");
    }

}
