using UnityEngine;

namespace sugi
{
    public class PooledEnemy : MonoBehaviour
    {
        private EnemyPool _pool;

        public void SetPool(EnemyPool pool)
        {
            _pool = pool;
        }

        public void Release()
        {
            _pool.ReturnPool(this);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject
                .TryGetComponent(out PlayerControll pc)) return;

            Release();
        }
    }
}
