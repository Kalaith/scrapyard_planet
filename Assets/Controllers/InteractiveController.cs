using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveController : MonoBehaviour {

    public Sprite itemSprite;

    List<GameObject> item_go_list;
    MapController mc;

    // Use this for initialization
    void Start () {
        mc = (MapController)FindObjectOfType(typeof(MapController));

    }

    public void addItem(Tile t) {

        GameObject item_go = new GameObject();

        item_go_list.Add(item_go);

        item_go.name = "Item_" + t.X + "_" + t.Y;
        item_go.transform.position = new Vector3(t.X, t.Y, 0);
        item_go.transform.SetParent(this.transform, true);
        item_go.AddComponent<SpriteRenderer>();
        item_go.GetComponent<SpriteRenderer>().sprite = itemSprite;
        item_go.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
