using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet {

    private GameObject _BulletGO;
    private GameObject _Target;
    private float _Speed;

    public Bullet(float speed, GameObject bulletGO, GameObject Target)
    {
        _Target = Target;
        _Speed = speed;
        _BulletGO = bulletGO;
    }

    public void Update()
    {
        _BulletGO.transform.position = Vector3.MoveTowards(_BulletGO.transform.position, _Target.transform.position, _Speed*Time.deltaTime);
    }

}
