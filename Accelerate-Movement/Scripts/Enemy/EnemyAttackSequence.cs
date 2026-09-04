using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using LitMotion;
using NaughtyAttributes;
using R3;
using Unity.Mathematics;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class EnemyAttackSequence : MonoBehaviour
{
    private CancellationTokenSource _cts;
    [Header("Scripts")]
    [SerializeField] private PlayerControll _playerControll;
    [Header("Prefab")]
    [SerializeField] private AttackMainLine _attackLine;
    [Header("Component")]
    [SerializeField] private Rigidbody2D _rb;
    [Header("Property")]
    [SerializeField] private float _minActionTime = 2f;
    [SerializeField] private float _maxActionTime = 5f;
    [SerializeField] private float _editerPlayerSpeed = 10f;
    [SerializeField, Range(0f, 100f)] private float _subAccelPercent = 50f;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _DisappearRange = 15f;

    [SerializeField] private float _distanceX = 15f;
    private readonly ReactiveProperty<float> _monitorPlayerSpeed = new();
    private IDisposable _subscription = null;
    private float _enemyAccel = 1f;
    private bool _isAttack = false;

    public void SetPlayerControll(PlayerControll playerControll)
    {
        _playerControll = playerControll;
    }

    void SetEnemySpeed()
    {
        if (_playerControll is null)
        {
            _monitorPlayerSpeed.Value = _editerPlayerSpeed;
        }
        else
        {
            _monitorPlayerSpeed.Value = _playerControll.MoveSpeed;
        }
        
    }

    void OnEnable()
    {
        _subscription = _monitorPlayerSpeed
            .DistinctUntilChanged()
            .Subscribe(s => _enemySpeed = s);

        SetEnemySpeed();
        _cts = new();
        float actionWaitTime = _maxActionTime;

        AnimationAsync(actionWaitTime, _cts.Token).Forget();
    }

    private async UniTask AnimationAsync(float actionWaitTime, CancellationToken token)
    {
        await _attackLine.AnimationAsync(
            token, 
            actionWaitTime / 2, 
            actionWaitTime / 2);
        _attackLine.transform.parent = null;
        _attackLine.FadeOut(token, actionWaitTime/ 8).Forget();
        BeEnemyAttackSpeed();
    }

    /// <summary>
    /// 減速させる
    /// </summary>
    private void BeEnemyAttackSpeed()
    {
        float max = 100f;
        _enemyAccel *= _subAccelPercent / max;
        Debug.Log($"減速完了: {_enemyAccel * _enemySpeed}");

        _isAttack = true;
    }

    void Update()
    {
        SetEnemySpeed();

        if (!_isAttack) return;
        if (_playerControll is null) return;

        if (_playerControll.transform.position.x 
            - math.abs(_DisappearRange) < this.transform.position.x)
            return;

        this.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        _rb.linearVelocityX = _enemySpeed * _enemyAccel;
    }

    void OnDisable()
    {
        _cts?.Cancel();
        _enemyAccel = 1f;
        _subscription?.Dispose();
    }
}
