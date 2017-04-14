using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {

    Vector3 lastFramePosition;
    Vector3 currentFramePosition;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        currentFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseDrag();

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }

    void mouseDrag() {
        if (Input.GetMouseButton(2)) {
            Vector3 diff = lastFramePosition - currentFramePosition;
            Camera.main.transform.Translate(diff);
        }
    }
}
