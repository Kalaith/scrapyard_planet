using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy {

    private GameObject _EnemyGO;
    private GameObject _EnemyGOSprite;
    private GameObject _Core;

    private int _Type;
    private int _Health;
    private bool _Dead;
    private int _Material;
    private int _EnemyCoreDamage;
    private int _EnemyAttackSpeed;
    private int explode = 1;
    private int max_explode = 7;
    private float max_frame = 0.05f;
    private float current_frame = 0;

    public Enemy(int type, int health, int material, GameObject enemy, GameObject enemysprite, GameObject core)
    {
        _Type = type;
        _Health = health;
        _EnemyGO = enemy;
        EnemyGOSprite = enemysprite;
        _Core = core;
        Dead = false;
        _Material = material;

        _EnemyCoreDamage = 1;
    }

    public int X {
        get { return Mathf.FloorToInt(_EnemyGO.transform.position.x+0.5f); }
    }

    public int Y {
        get { return Mathf.FloorToInt(_EnemyGO.transform.position.y + 0.5f); } 
    }

    public void Update(float Speed) {
        if (current_frame > max_frame) {
            //Debug.Log("My Health: "+ _Health+" Statues"+_Dead);
            if (_Health <= 0) {
                if(_EnemyGOSprite.activeSelf) {
                    max_frame = 0.2f;
                    EnemyGOSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load("Enemies/boom" + explode, typeof(Sprite)) as Sprite;
                    explode++;
                    if (explode == max_explode) {
                        _EnemyGOSprite.SetActive(false);
                    }
                }
            } else {
                _EnemyGO.transform.position = Vector3.MoveTowards(_EnemyGO.transform.position, _Core.transform.position, Speed * max_frame);

                Vector3 moveDirection = _EnemyGO.transform.position - _Core.transform.position;

                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                _EnemyGOSprite.transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);


            }
            current_frame = 0;
        }
        current_frame += Time.deltaTime;


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

    public int EnemyCoreDamage {
        get {
            return _EnemyCoreDamage;
        }

        set {
            _EnemyCoreDamage = value;
        }
    }

    public int EnemyAttackSpeed {
        get {
            return _EnemyAttackSpeed;
        }

        set {
            _EnemyAttackSpeed = value;
        }
    }

    public GameObject EnemyGOSprite {
        get {
            return _EnemyGOSprite;
        }

        set {
            _EnemyGOSprite = value;
        }
    }
}