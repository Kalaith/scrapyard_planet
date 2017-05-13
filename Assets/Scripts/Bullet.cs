using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private GameObject _Target;
    private float _Speed;

    public void BulletInit(GameObject Target, float Speed)
    {
        _Target = Target;
        _Speed = Speed;
    }

	void Update ()
    {
        transform.position = Vector3.MoveTowards(transform.position, _Target.transform.position, _Speed*Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("collision found!");
            Destroy(col.gameObject);
            Destroy(gameObject);
    }

}
