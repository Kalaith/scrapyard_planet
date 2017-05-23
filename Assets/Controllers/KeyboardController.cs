using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour {

    GameController gc;
    PlayerController p;

    private Sprite button_on;
    private Sprite button_off;
    string itemName = "";

    // Use this for initialization
    void Start () {
        gc = (GameController)FindObjectOfType(typeof(GameController));
        p = (PlayerController)FindObjectOfType(typeof(PlayerController));

        button_on = Resources.Load("switchon", typeof(Sprite)) as Sprite;
        button_off = Resources.Load("switchoff", typeof(Sprite)) as Sprite;
}

// Update is called once per frame
void Update () {

        if (Input.GetKeyDown("s")) {
            gc.StartCountDown = false;
        }


        if (Input.GetKeyDown("r")) {
            if (p.Player.CurrTile.Item != null && p.Player.CurrTile.Item.needsRepair()) {
                if (gc.Materials < p.Player.CurrTile.Item.RepairCost) {
                    Debug.Log("Not enough materials, materials remaining "+ gc.Materials);
                } else {
                    gc.spendMaterials(p.Player.CurrTile.Item.RepairCost);
                    p.Player.CurrTile.Item.repairItem();
                    Debug.Log("materials remaining " + gc.Materials);
                }
                if(p.Player.CurrTile.Item.RepairProgress == 100) {
                    // if we have repaired the switch, update the game object switch at the location we have done the repairs.
                    
                    if (p.Player.CurrTile.Item.GetType() == typeof(Engine)) {
                        itemName = "Engine_" + p.Player.CurrTile.X + "_" + p.Player.CurrTile.Y;
                    } else {
                        itemName = "Item_" + p.Player.CurrTile.X + "_" + p.Player.CurrTile.Y;
                    }
                    GameObject.Find(itemName).GetComponent<SpriteRenderer>().sprite = button_on;
                }
                
                Debug.Log("Repairing item, perc complete: "+p.Player.CurrTile.Item.RepairProgress);
            } else if(p.Player.CurrTile.Item != null && !p.Player.CurrTile.Item.needsRepair()) {
                Debug.Log("Item is already repaired.");
            } else {
                Debug.Log("No Item to repair.");
            }

        }
        if (Input.GetKeyDown("o")) {
            if (p.Player.CurrTile.Item != null) {
                if(p.Player.CurrTile.Item.Status==InteractiveItem.InteractiveStatus.On) {
                    p.Player.CurrTile.Item.turnOff();
                    if (p.Player.CurrTile.Item.GetType() == typeof(Engine)) {
                        itemName = "Engine_" + p.Player.CurrTile.X + "_" + p.Player.CurrTile.Y;
                    } else {
                        itemName = "Item_" + p.Player.CurrTile.X + "_" + p.Player.CurrTile.Y;
                    }
                    GameObject.Find(itemName).GetComponent<SpriteRenderer>().sprite = button_off;
                    Debug.Log("Switch Off.");
                    gc.spendMaterials(10);
                } else if (p.Player.CurrTile.Item.Status == InteractiveItem.InteractiveStatus.Off) {
                    p.Player.CurrTile.Item.turnOn();
                    if (p.Player.CurrTile.Item.GetType() == typeof(Engine)) {
                        itemName = "Engine_" + p.Player.CurrTile.X + "_" + p.Player.CurrTile.Y;
                    } else {
                        itemName = "Item_" + p.Player.CurrTile.X + "_" + p.Player.CurrTile.Y;
                    }
                    GameObject.Find(itemName).GetComponent<SpriteRenderer>().sprite = button_on;
                    Debug.Log("Switch On.");
                    gc.spendMaterials(10);
                } else if (p.Player.CurrTile.Item.Status == InteractiveItem.InteractiveStatus.Damaged) {
                    Debug.Log("Switch needs to be repaired.");
                } else {
                    Debug.Log("Switch is disabled.");
                }
            }
        }
    }
}
