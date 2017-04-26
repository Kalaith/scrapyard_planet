using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private int _Type;
    private int _Health;
    private Vector2 _SpawnPos;
    private float _SlopeToShip;
    public int Speed;

    public Enemy(int t, int h, Vector2 s)
    {
        _Type = t;
        _Health = h;
        _SpawnPos = s;

        GameObject x = new GameObject("Enemy");

    }

    public float CalculateSlope()
    {
        return _SpawnPos[1] / _SpawnPos[0];
    }
		
    void Update ()
    {
        if (this.transform.localPosition.x > 0)
        {
            this.transform.Translate(-(Speed), _SlopeToShip, 0);
        } else
        {
            this.transform.Translate(Speed, _SlopeToShip, 0);
        }
    }
	
}
