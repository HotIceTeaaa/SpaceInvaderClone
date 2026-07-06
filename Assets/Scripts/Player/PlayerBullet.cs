using UnityEngine;

namespace SpaceInvader
{
    public class PlayerBullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;

        void Update()
        {
            _rb.linearVelocity = transform.up * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if(collision.CompareTag("EnemyBullet")) {
                PoolManager.Instance.Return(BulletType.Player, gameObject);
            }

            if (collision.CompareTag("Enemy")) {
                Enemy enemyScript = collision.GetComponent<Enemy>();
                enemyScript.HandleEnemyTakeDamage(_damage);

                PoolManager.Instance.Return(BulletType.Player, gameObject);
            }
        }
    }
}
