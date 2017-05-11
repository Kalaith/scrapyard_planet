using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour {

    GameController gc;
    PlayerController p;

	// Use this for initialization
	void Start () {
        gc = (GameController)FindObjectOfType(typeof(GameController));
        p = (PlayerController)FindObjectOfType(typeof(PlayerController));
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("r")) {
            if (p.Player.CurrTile.Item != null && p.Player.CurrTile.Item.needsRepair()) {
                if (gc.Materials < p.Player.CurrTile.Item.RepairCost) {
                    Debug.Log("Not enough materials, materials remaining "+ gc.Materials);
                } else {
                    gc.spendMaterials(p.Player.CurrTile.Item.RepairCost);
                    p.Player.CurrTile.Item.repairItem();
                    Debug.Log("materials remaining " + gc.Materials);
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
                    Debug.Log("Switch Off.");
                } else if (p.Player.CurrTile.Item.Status == InteractiveItem.InteractiveStatus.Off) {
                    p.Player.CurrTile.Item.turnOn();
                    Debug.Log("Switch On.");
                } else if (p.Player.CurrTile.Item.Status == InteractiveItem.InteractiveStatus.Damaged) {
                    Debug.Log("Switch needs to be repaired.");

                } else {
                    Debug.Log("Switch is disabled.");
                }
            }
        }
    }
}
