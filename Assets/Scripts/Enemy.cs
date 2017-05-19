using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy {

    private GameObject _EnemyGO;
    private GameObject _Core;

    private int _Type;
    private int _Health;
    private bool _Dead;
    private int _Material;

    public Enemy(int type, int health, int material, GameObject enemy, GameObject core)
    {
        _Type = type;
        _Health = health;
        _EnemyGO = enemy;
        _Core = core;
        Dead = false;
        _Material = material;
    }

    public int X {
        get { return Mathf.FloorToInt(_EnemyGO.transform.position.x+0.5f); }
    }

    public int Y {
        get { return Mathf.FloorToInt(_EnemyGO.transform.position.y + 0.5f); }
    }

    public void Update(float Speed) {
        Debug.Log("My Health: "+ _Health+" Statues"+_Dead);
        if (_Health <= 0) {
            _EnemyGO.SetActive(false);
        } else {
            _EnemyGO.transform.position = Vector3.MoveTowards(_EnemyGO.transform.position, _Core.transform.position, Speed * Time.deltaTime);
        }
    }

    public int Health {
        get { return _Health; }
        set { _Health = value; }
    }

    public GameObject EnemyGO {
        get { return _EnemyGO; }
        set { _EnemyGO = value; }
    }

    public bool Dead {
        get {
            return _Dead;
        }

        set {
            _Dead = value;
        }
    }

    public int Material {
        get {
            return _Material;
        }

        set {
            _Material = value;
        }
    }
}