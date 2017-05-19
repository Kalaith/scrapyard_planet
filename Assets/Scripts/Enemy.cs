using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : IDisposable {

    private GameObject _EnemyGO;
    private GameObject _Core;

    private int _Type;
    private int _Health;

    public Enemy(int type, int health, GameObject enemy, GameObject core)
    {
        _Type = type;
        _Health = health;
        _EnemyGO = enemy;
        _Core = core;
    }

    public void Dispose() {

    }

    public int X {
        get { return Mathf.FloorToInt(_EnemyGO.transform.position.x+0.5f); }
    }

    public int Y {
        get { return Mathf.FloorToInt(_EnemyGO.transform.position.y + 0.5f); }
    }

    public void Update(float Speed) {
        if (_Health == 0) {
            _EnemyGO.SetActive(false);
            this.Dispose();
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
}