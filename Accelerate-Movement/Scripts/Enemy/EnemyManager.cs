using System.Collections.Generic;
using System.Threading;
using sugi;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyManager : EnemyPool
{
    CancellationTokenSource _cts;
    [SerializeField] private PlayerControll _playerControll;
    [SerializeField] private float _distanceX = 15f;

    protected override void Start()
    {
        base.Start();
        _cts = new();

        _playerControll = FindAnyObjectByType<PlayerControll>();
    }

    public void SetEnemy()
    {
        PooledEnemy enemy = GetPooeldEnemy();
        if (enemy.gameObject.TryGetComponent(out EnemyAttackSequence component))
        {
            component.SetPlayerControll(_playerControll);
        }

        var targetPos = _playerControll.transform.position;
        targetPos.x += _distanceX;
        targetPos.y += UnityEngine.Random.Range(-5f, 5f);

        enemy.transform.position = targetPos;
    }
}