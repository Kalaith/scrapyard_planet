using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    List<GameObject> Enemies = new List<GameObject>();
    public float EnemySpeed;
    public GameObject Enemy1;
    public Transform EnemyParent;
    public Transform Target;


    // Use this for initialization
    void Start () {
        for (int x = 0; x < 20; x++)
        {
            Vector3 position = new Vector3(Target.position.x + Random.Range(-2.8f, 2.8f), Target.position.y + Random.Range(-4.65f, 4.65f), 0);
            GameObject pre = Instantiate(Enemy1, position, Quaternion.identity);
            pre.name = "Enemy " + x;
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
