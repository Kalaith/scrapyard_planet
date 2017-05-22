using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(TitleScreenDisplay());
    }
	
	// Update is called once per frame
	IEnumerator TitleScreenDisplay() {
        
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("PlayerScene");
    }

   
}
