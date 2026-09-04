using System.Collections.Generic;
using UnityEngine;

namespace sugi
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private int _initCount = 20;
        [SerializeField] private PooledEnemy _pooled;
        private static Stack<PooledEnemy> _Pool;

        protected virtual void Start()
        {
            SetUp();
        }

        private void SetUp()
        {
            _Pool = new();
            PooledEnemy instance = null;
            for (int i = 0; i < _initCount; i++)
            {
                instance = Instantiate(_pooled, this.transform);
                instance.SetPool(this);
                instance.gameObject.SetActive(false);
                _Pool.Push(instance);
            }

        }

        public PooledEnemy GetPooeldEnemy()
        {
            if (_Pool.Count <= 0)
            {
                var instance = Instantiate(_pooled, this.transform);
                instance.SetPool(this);
                instance.gameObject.SetActive(true);
                return instance;
            }

            var pooled = _Pool.Pop();
            pooled.gameObject.SetActive(true);
            return pooled;
        }

        public void ReturnPool(PooledEnemy pooled)
        {
            pooled.gameObject.SetActive(false);
            _Pool.Push(pooled);
        }
    }
}