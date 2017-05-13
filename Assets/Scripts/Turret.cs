using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private InteractiveItem _Switch;
    private string _Type;
    public List<GameObject> Targets = new List<GameObject>();
    public GameObject bullet;
    public int BullSpeed;
    public int BullPS;

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
        //if (_Switch.Status == InteractiveItem.InteractiveStatus.On)
        //{
        if (BullPS <= 0)
        {
            if (Targets.Count > 0)
            {
                GameObject bull = Instantiate(bullet, this.transform.parent);
                bull.GetComponent<Bullet>().BulletInit(Targets[0], BullSpeed);
                BullPS = 1000;
            }
        }
            
        BullPS -= 1;
        //}
	}
}
