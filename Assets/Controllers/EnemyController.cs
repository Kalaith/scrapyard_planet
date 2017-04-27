using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    List<GameObject> Enemies = new List<GameObject>();
    public float EnemySpeed;
    public GameObject Enemy1;
    public Transform EnemyParent;


    // Use this for initialization
    void Start () {
        for (int x = 0; x < 5; x++)
        {
            Vector3 position = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0);
            GameObject pre = Instantiate(Enemy1, position, Quaternion.identity);
            Enemies.Add(pre);
            pre.transform.parent = EnemyParent;
        }
    }
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject x in Enemies)
        {
            x.GetComponent<Enemy>().MoveEnemies(EnemySpeed);
        }
		
	}
}
