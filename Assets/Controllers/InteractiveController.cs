﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveController : MonoBehaviour {

    List<GameObject> item_go_list;
    List<InteractiveItem> item_list;
    GameController gc;
    // Use this for initialization
    void Start () {
        item_go_list = new List<GameObject>();
        
        gc = (GameController)FindObjectOfType(typeof(GameController));
    }


    public void addTurret(Turret t, int x, int y, Sprite turrentSprite) {
        if (turrets == null) {
            turrets = new List<Turret>();
        }
            turrets.Add(t);

        GameObject turret_go = new GameObject();
        if (turret_go_list == null) {
            turret_go_list = new List<GameObject>();
        }
        turret_go_list.Add(turret_go);

        turret_go.name = "Item_" + x + "_" + y;
        turret_go.transform.position = new Vector3(x, y, 0);
        turret_go.transform.SetParent(this.transform, true);
        turret_go.AddComponent<SpriteRenderer>();
        turret_go.GetComponent<SpriteRenderer>().sprite = turrentSprite;
        turret_go.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    public void addItem(InteractiveItem item, int x, int y, Sprite itemSprite) {

        if (item_list == null) {
            item_list = new List<InteractiveItem>();
        }
        item_list.Add(item);

        GameObject item_go = new GameObject();
        if (item_go_list == null) {
            item_go_list = new List<GameObject>();
        }
        item_go_list.Add(item_go);

        item_go.name = "Item_" + x + "_" + y;
        item_go.transform.position = new Vector3(x, y, 0);
        item_go.transform.SetParent(this.transform, true);
        item_go.AddComponent<SpriteRenderer>();
        item_go.GetComponent<SpriteRenderer>().sprite = itemSprite;
        item_go.GetComponent<SpriteRenderer>().sortingOrder = 1;

    }

    List<GameObject> turret_go_list;
    private List<Turret> turrets;
    private GameObject bullet;

    // Update is called once per frame
    void Update () {
        // Instead of setting the power usage back and forth, every update just query every item for its current power usage.
        gc.ResetPowerUsage();
		foreach(InteractiveItem item in item_list) {
            if (item.Status == InteractiveItem.InteractiveStatus.On) {
                gc.increasePowerUsage(item.Operational_power_usage, item.Reserved_power_usage);
            } else if (item.Status == InteractiveItem.InteractiveStatus.Off) {
                gc.increasePowerUsage(0, item.Reserved_power_usage);
            }
        }

        foreach (Turret turret in turrets) {
            if (turret.Status == InteractiveItem.InteractiveStatus.On) {
                GameObject bull = Instantiate(bullet, this.transform.parent);
                bull.GetComponent<Bullet>().BulletInit(turret.CurrentTarget(), 100);
                turret.BullPS = 1000;

            }
        }
            // Temporary, when the power core is installed it starts usinbg power to try and turn on the ship.
            // This should be a better object then hacking it onto this.
            gc.increasePowerUsage(0, 100);
    }
}
