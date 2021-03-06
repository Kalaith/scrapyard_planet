﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveController : MonoBehaviour {

    private List<Turret> _Turrets;
    private List<Bullet> _Bullets;
    List<GameObject> item_go_list;
    List<InteractiveItem> item_list;
    List<GameObject> engine_go_list;
    List<InteractiveItem> engine_list;
    GameController gc;
    EnemyController ec;
    // Use this for initialization
    void Start () {

       
        gc = (GameController)FindObjectOfType(typeof(GameController));
        ec = (EnemyController)FindObjectOfType(typeof(EnemyController));
    }


    public void addTurret(Turret t, int x, int y, Sprite turrentSprite) {
        if (_Turrets == null) {
            _Turrets = new List<Turret>();
        }
        _Turrets.Add(t);

        GameObject turret_go = new GameObject();
        turret_go.name = "TurretBase_" + x + "_" + y;
        turret_go.transform.position = new Vector3(x, y, 0);
        turret_go.transform.SetParent(this.transform, true);
        turret_go.AddComponent<SpriteRenderer>();
        turret_go.GetComponent<SpriteRenderer>().sprite = turrentSprite;
        turret_go.GetComponent<SpriteRenderer>().sortingOrder = 1;
        turret_go.AddComponent<CircleCollider2D>();
        turret_go.GetComponent<CircleCollider2D>().radius = 2f;

        t.TurrentGO = turret_go;

        GameObject turret_go_canon = new GameObject();
        turret_go_canon.name = "TurretCanon_" + x + "_" + y;
        turret_go_canon.transform.position = new Vector3(x+0.5f, y+0.5f, 0);
        turret_go_canon.transform.SetParent(turret_go.transform, true);
        turret_go_canon.AddComponent<SpriteRenderer>();
        turret_go_canon.GetComponent<SpriteRenderer>().sprite = Resources.Load("ExternalTiles/turrettop1", typeof(Sprite)) as Sprite;
        turret_go_canon.GetComponent<SpriteRenderer>().sortingOrder = 2;
        turret_go_canon.AddComponent<CircleCollider2D>();
        turret_go_canon.GetComponent<CircleCollider2D>().radius = 3f;
        turret_go_canon.SetActive(false);

        t.TurrentGOCanon = turret_go_canon;
    }

    public void addItem(InteractiveItem item, int x, int y, Sprite itemSprite) {

        if (item_list == null) {
            item_list = new List<InteractiveItem>();
        }
        item_list.Add(item);

        GameObject item_go = new GameObject();
        if (item_go_list == null) {
            item_go_list = new List<GameObject>();
        }
        item_go_list.Add(item_go);

        item_go.name = "Item_" + x + "_" + y;
        item_go.transform.position = new Vector3(x, y, 0);
        item_go.transform.SetParent(this.transform, true);
        item_go.AddComponent<SpriteRenderer>();
        item_go.GetComponent<SpriteRenderer>().sprite = itemSprite;
        item_go.GetComponent<SpriteRenderer>().sortingOrder = 1;

    }

    public void addEngine(InteractiveItem item, int x, int y, Sprite itemSprite) {

        if (engine_list == null) {
            engine_list = new List<InteractiveItem>();
        }
        if (engine_go_list == null) {
            engine_go_list = new List<GameObject>();
        }

        engine_list.Add(item);

        GameObject engine_go = new GameObject();

        engine_go_list.Add(engine_go);

        engine_go.name = "Engine_" + x + "_" + y;
        engine_go.transform.position = new Vector3(x, y, 0);
        engine_go.transform.SetParent(this.transform, true);
        engine_go.AddComponent<SpriteRenderer>();
        engine_go.GetComponent<SpriteRenderer>().sprite = itemSprite;
        engine_go.GetComponent<SpriteRenderer>().sortingOrder = 1;

    }

    public void assignTarget(Enemy enemy) {
        foreach (Turret turret in _Turrets) {
            turret.addTarget(enemy);
        }
    }


    private float max_frame = 0.05f;
    private float current_frame = 0;

    // Update is called once per frame
    void Update () {
        // Stop running if its game over.
        if (gc != null && ec != null && !gc.GameOver) {

            gc.ResetPowerUsage();

            updateItems();
            updateEngines();
            updateTurrets();

            if (current_frame > max_frame) {
                updateBullets();

                current_frame = 0;
            }
            current_frame += Time.deltaTime;

            // Temporary, when the power core is installed it starts using power to try and turn on the ship.
            // This should be a better object then hacking it onto this.
            gc.increasePowerUsage(0, 100);
        }



    }

    private void updateBullets() {
        // Create a bullet list if it doesn't exist
        if (_Bullets == null) {
            _Bullets = new List<Bullet>();
        }

        // Update all bullets.
        foreach (Bullet bullet in _Bullets) {
            bullet.Update(max_frame);
        }


        // go through each bullet and remove if it hit a target
        foreach (Bullet bullet in _Bullets) {
            if (bullet.TargetHit) {
                _Bullets.Remove(bullet);
                bullet.Dispose();
                break;
            }
        }
    }

    private float last_fired = 0;

    private void updateTurrets() {
        foreach (Turret turret in _Turrets) {

            if (turret.Status == InteractiveItem.InteractiveStatus.On) {
                turret.TurrentGO.GetComponent<SpriteRenderer>().sprite = Resources.Load("ExternalTiles/turretbase1", typeof(Sprite)) as Sprite;
                turret.TurrentGOCanon.GetComponent<SpriteRenderer>().sprite = Resources.Load("ExternalTiles/turrettop2", typeof(Sprite)) as Sprite;
                turret.TurrentGOCanon.SetActive(true);


                // We want to find all enemies that are in range of the turrent 
                turret.EnemyTargets = ec.InRange(turret.TurrentGO.transform.position, turret.Range);
                if (last_fired > turret.BullPS) {
                    //Debug.Log("Turrent Firing");
                    Enemy target = turret.CurrentTarget();

                    if (target != null && !target.Dead) {

                        Vector3 moveDirection = turret.TurrentGO.transform.position - target.EnemyGO.transform.position;

                        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

                        turret.TurrentGOCanon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


                        GameObject bulletGO = new GameObject(); ;
                        bulletGO.name = "Bullet_" + target.ToString();
                        Vector3 position = new Vector3(turret.TurrentGO.transform.position.x + 0.5f, turret.TurrentGO.transform.position.y + 0.5f, 0);

                        bulletGO.transform.position = position;
                        bulletGO.transform.SetParent(this.transform, true);
                        bulletGO.AddComponent<SpriteRenderer>();
                        bulletGO.GetComponent<SpriteRenderer>().sprite = Resources.Load("rocket_32px", typeof(Sprite)) as Sprite;
                        bulletGO.GetComponent<SpriteRenderer>().sortingOrder = 3;

                        bulletGO.AddComponent<CircleCollider2D>();
                        bulletGO.GetComponent<CircleCollider2D>().radius = 0.2f;

                        _Bullets.Add(new Bullet(2, bulletGO, target));

                    }
                    last_fired = 0;
                }
                last_fired += Time.deltaTime;
            } else if(turret.Status == InteractiveItem.InteractiveStatus.Off) {
                turret.TurrentGO.GetComponent<SpriteRenderer>().sprite = Resources.Load("ExternalTiles/turretbase1", typeof(Sprite)) as Sprite;
                turret.TurrentGOCanon.GetComponent<SpriteRenderer>().sprite = Resources.Load("ExternalTiles/turrettop1", typeof(Sprite)) as Sprite;
                //turret.TurrentGOCanon.GetComponent<SpriteRenderer>().sortingOrder = 3;
                turret.TurrentGOCanon.SetActive(true);
            }



        }
    }

    private void updateItems() {
        // Instead of setting the power usage back and forth, every update just query every item for its current power usage.

        if (item_list == null) {
            item_list = new List<InteractiveItem>();
        }

        foreach (InteractiveItem item in item_list) {
            if (item.Status == InteractiveItem.InteractiveStatus.On) {
                gc.increasePowerUsage(item.Operational_power_usage, item.Reserved_power_usage);
            } else if (item.Status == InteractiveItem.InteractiveStatus.Off) {
                gc.increasePowerUsage(0, item.Reserved_power_usage);
            }
        }
    }

    private void updateEngines() {

        if (engine_list == null) {
            engine_list = new List<InteractiveItem>();
        }

        // If all engines are turned on, start the countdown.
        int engineCount = 0;
        foreach (InteractiveItem item in engine_list) {
            if (item.Status == InteractiveItem.InteractiveStatus.On) {
                engineCount++;
                gc.increasePowerUsage(item.Operational_power_usage, item.Reserved_power_usage);
            } else if (item.Status == InteractiveItem.InteractiveStatus.Off) {
                gc.increasePowerUsage(0, item.Reserved_power_usage);
            }
        }

        // Start the coutndown if all engines are on, otherwise pause the countdown.
        if (engineCount == engine_list.Count) {
            gc.StartCountDown = true;
        } else {
            gc.StartCountDown = false;
        }
    }
}
