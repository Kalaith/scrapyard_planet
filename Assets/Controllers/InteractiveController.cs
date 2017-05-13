using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveController : MonoBehaviour {

    public Sprite itemSprite;

    List<GameObject> item_go_list;

    // Use this for initialization
    void Start () {
        item_go_list = new List<GameObject>();
    }

    public void addItem(int x, int y) {

        GameObject item_go = new GameObject();

        item_go_list.Add(item_go);
        item_go.name = "Item_" + x + "_" + y;
        item_go.transform.position = new Vector3(x, y, 0);
        item_go.transform.SetParent(this.transform, true);
        item_go.AddComponent<SpriteRenderer>();
        item_go.GetComponent<SpriteRenderer>().sprite = itemSprite;
        item_go.GetComponent<SpriteRenderer>().sortingOrder = 1;
        Debug.Log("Game Object created for itemSprite");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
