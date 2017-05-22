using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public List<Enemy> _Enemies = new List<Enemy>();
    public float _EnemySpeed;

    private List<Turret> _Turrets;

    private MapController mc;
    private InteractiveController ic;
    private GameController gc;

    public void addTurret(Turret turrent) {
        _Turrets.Add(turrent);
    }

    // Use this for initialization
    void Start () {

        mc = (MapController)FindObjectOfType(typeof(MapController));
        ic = (InteractiveController)FindObjectOfType(typeof(InteractiveController));
        gc = (GameController)FindObjectOfType(typeof(GameController));

        for (int i = 0; i < 20; i++)
        {
            int x = 0;
            int y = 0;

            int direction = Random.Range(4, 1);
            if(direction == 1 ) {
            // Spawn from the left side
                x = Random.Range(mc.startx - 1, mc.startx);
                y = Random.Range(0, mc.ExternalMap.Height);
            }

            if (direction == 2) {
                // Spawn from the right side
                x = Random.Range(mc.startx + mc.ExternalMap.Width, mc.startx + mc.ExternalMap.Width + 1);
                y = Random.Range(0, mc.ExternalMap.Height);
            }

            if (direction == 3) {
                // Spawn from top
                y = Random.Range(mc.ExternalMap.Height + 1, mc.ExternalMap.Height);
                x = Random.Range(mc.startx, mc.startx + mc.ExternalMap.Width);
            }

            if (direction == 4) {
                // Spawn from bottom
                y = Random.Range(0, -1);
                x = Random.Range(mc.startx, mc.startx + mc.ExternalMap.Width);
            }

            Vector3 position = new Vector3(x, y, 0);
            GameObject enemyGO = new GameObject(); ;
            enemyGO.name = "Enemy_" + x;
            enemyGO.transform.position = position;
            enemyGO.transform.SetParent(this.transform, true);
            enemyGO.AddComponent<SpriteRenderer>();
            enemyGO.GetComponent<SpriteRenderer>().sprite = Resources.Load("Enemies/smallbotbase", typeof(Sprite)) as Sprite;
            enemyGO.GetComponent<SpriteRenderer>().sortingOrder = 1;
            enemyGO.AddComponent<CircleCollider2D>();
            enemyGO.GetComponent<CircleCollider2D>().radius = 0.3f;
            enemyGO.GetComponent<CircleCollider2D>().isTrigger = true;

            Enemy e = new Enemy(1, 1, 10, enemyGO, mc.Core);
            _Enemies.Add(e);

            ic.assignTarget(e);
        }
    }
	
	// Update is called once per frame
	void Update () {

        foreach (Enemy enemy in _Enemies) {
            Tile tile = mc.ExternalMap.GetTileAt(enemy.X - mc.startx, enemy.Y);

            if (tile != null) {

                if (tile.Cost == 9) {
                    // Are we on a tile that is part of the ship? deal damage
                    if (enemy.EnemyAttackSpeed <= 0) {
                        gc.damageCore(enemy.EnemyCoreDamage);
                        enemy.EnemyAttackSpeed = 200;
                    }
                    enemy.EnemyAttackSpeed--;
                    enemy.Update(_EnemySpeed * -2);
                } else {
                    enemy.Update(_EnemySpeed);
                }
            } else {

                enemy.Update(_EnemySpeed);
            }

            if (enemy.Health <= 0 && !enemy.Dead) {
                gc.increaseMaterials(enemy.Material);
                enemy.Dead = true;
            }

        }
	}

    public List<Enemy> InRange(Vector3 position, int range) {
        List<Enemy> inRange = new List<Enemy>();

        // Currently loops and compares by the range, would like if this was converted from range to radius.
        foreach (Enemy enemy in _Enemies) {
            // We do not want to add dead enemies.
            if (enemy.Health > 0 && !enemy.Dead) {
                if (enemy.EnemyGO.transform.position.x >= (position.x - range) && enemy.EnemyGO.transform.position.x <= (position.x + range)) {
                    if (enemy.EnemyGO.transform.position.y >= (position.y - range) && enemy.EnemyGO.transform.position.y <= (position.y + range)) {
                        inRange.Add(enemy);
                    }
                }
            }
        }
        return inRange;
    }
}
