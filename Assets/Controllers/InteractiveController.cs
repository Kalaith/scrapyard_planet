using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveController : MonoBehaviour {

    private List<Turret> _Turrets;
    private List<Bullet> _Bullets;
    List<GameObject> item_go_list;
    List<InteractiveItem> item_list;
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
        turret_go.name = "Item_" + x + "_" + y;
        turret_go.transform.position = new Vector3(x, y, 0);
        turret_go.transform.SetParent(this.transform, true);
        turret_go.AddComponent<SpriteRenderer>();
        turret_go.GetComponent<SpriteRenderer>().sprite = turrentSprite;
        turret_go.GetComponent<SpriteRenderer>().sortingOrder = 1;
        turret_go.AddComponent<CircleCollider2D>();
        turret_go.GetComponent<CircleCollider2D>().radius = 3f;

        t.TurrentGO = turret_go;
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

    public void assignTarget(Enemy enemy) {
        foreach (Turret turret in _Turrets) {
            turret.addTarget(enemy);
        }
    }


    // Update is called once per frame
    void Update () {

        if (_Bullets == null) {
            _Bullets = new List<Bullet>();
        }
        
        // Instead of setting the power usage back and forth, every update just query every item for its current power usage.
        gc.ResetPowerUsage();
		foreach(InteractiveItem item in item_list) {
            if (item.Status == InteractiveItem.InteractiveStatus.On) {
                gc.increasePowerUsage(item.Operational_power_usage, item.Reserved_power_usage);
            } else if (item.Status == InteractiveItem.InteractiveStatus.Off) {
                gc.increasePowerUsage(0, item.Reserved_power_usage);
            }
        }

        foreach (Turret turret in _Turrets) {
            
            if (turret.Status == InteractiveItem.InteractiveStatus.On) {
                // We want to find all enemies that are in range of the turrent 
                turret.EnemyTargets = ec.InRange(turret.TurrentGO.transform.position, turret.Range);
                if (turret.BullPS <= 0) {
                    //Debug.Log("Turrent Firing");
                    Enemy target = turret.CurrentTarget();

                    if (target != null && !target.Dead) {

                        GameObject bulletGO = new GameObject(); ;
                        bulletGO.name = "Bullet_" + target.ToString();
                        Vector3 position = new Vector3(turret.TurrentGO.transform.position.x + 0.5f, turret.TurrentGO.transform.position.y + 0.5f, 0);

                        bulletGO.transform.position = turret.TurrentGO.transform.position;
                        bulletGO.transform.SetParent(this.transform, true);
                        bulletGO.AddComponent<SpriteRenderer>();
                        bulletGO.GetComponent<SpriteRenderer>().sprite = Resources.Load("placeBullet", typeof(Sprite)) as Sprite;
                        bulletGO.GetComponent<SpriteRenderer>().sortingOrder = 3;

                        bulletGO.AddComponent<CircleCollider2D>();
                        bulletGO.GetComponent<CircleCollider2D>().radius = 0.2f;

                        _Bullets.Add(new Bullet(2, bulletGO, target));
                        turret.BullPS = 200;
                    }
                }
            }
            turret.BullPS -= 1;

        }

        foreach (Bullet bullet in _Bullets) {
            bullet.Update();
        }

        foreach (Bullet bullet in _Bullets) {
            if (bullet.TargetHit) {
                _Bullets.Remove(bullet);
                bullet.Dispose();
                break;
            }
        }

        // Temporary, when the power core is installed it starts usinbg power to try and turn on the ship.
        // This should be a better object then hacking it onto this.
        gc.increasePowerUsage(0, 100);
    }
}
