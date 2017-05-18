using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet {

    private GameObject _BulletGO;
    private GameObject _Target;
    private float _Speed;

    public void BulletInit(GameObject Target, float Speed)
    {
        _Target = Target;
        _Speed = Speed;
    }

	void Update ()
    {
        _BulletGO.transform.position = Vector3.MoveTowards(_BulletGO.transform.position, _Target.transform.position, _Speed*Time.deltaTime);
    }

}
