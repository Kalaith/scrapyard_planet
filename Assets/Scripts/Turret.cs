using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private InteractiveItem _Switch;
    private string _Type;
    public List<Enemy> Targets = new List<Enemy>();

    public Turret(InteractiveItem Switch, string Type)
    {
        _Switch = Switch;
        _Type = Type;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
