using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private int _Type;
    private int _Health;
    public float _SpeedVariation;

    public Enemy(int t, int h, Vector2 s)
    {
        _Type = t;
        _Health = h;
        _SpeedVariation = Random.Range(0.75f, 1.5f);
    }
	
    public void MoveEnemies(float Speed)
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, GameObject.Find("OverviewShip").transform.position, (Speed * _SpeedVariation) * Time.deltaTime);
    }

}