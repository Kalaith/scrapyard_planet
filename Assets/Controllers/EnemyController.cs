using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public List<GameObject> Enemies = new List<GameObject>();
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

            if (GameObject.Find("OverviewShip").transform.position.y > pre.transform.position.y)
            {
                if (GameObject.Find("OverviewShip").transform.position.x > pre.transform.position.x)
                {
                    //add to turret firing list (bottom left)
                } else
                {
                    //add to turret firing list (bottom right)
                }
            } else
            {
                if (GameObject.Find("OverviewShip").transform.position.x > pre.transform.position.x)
                {
                    //add to turret firing list (top Left)
                }
                else
                {
                    //add to turret firing list (top right)
                }
            }
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
