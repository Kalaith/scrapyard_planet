﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : IDisposable {

    private GameObject _BulletGO;
    private Enemy _Target;
    private float _Speed;
    private bool _TargetHit;

    public bool TargetHit {
        get { return _TargetHit; }
    }

    public Bullet(float speed, GameObject bulletGO, Enemy Target)
    {
        _Target = Target;
        _Speed = speed;
        _BulletGO = bulletGO;
        _TargetHit = false;
    }

    public void Dispose() {

    }

    public void Update(float max_frame)
    {
        if (!_TargetHit) {
            Vector3 targetPosition = _Target.EnemyGO.transform.position;
            targetPosition.x += 0.5f;
            targetPosition.y += 0.5f;

            _BulletGO.transform.position = Vector3.MoveTowards(_BulletGO.transform.position, targetPosition, _Speed * max_frame);
            Vector3 moveDirection = _BulletGO.transform.position - targetPosition;

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;

            _BulletGO.transform.rotation = Quaternion.AngleAxis(angle + 180f, Vector3.forward);

            if (Mathf.FloorToInt(_BulletGO.transform.position.x) == Mathf.FloorToInt(targetPosition.x) && Mathf.FloorToInt(_BulletGO.transform.position.y) == Mathf.FloorToInt(targetPosition.y)) {
                _Target.Health = _Target.Health - 1;
                _TargetHit = true;
                _BulletGO.SetActive(false);
            }
        }
    }

}
