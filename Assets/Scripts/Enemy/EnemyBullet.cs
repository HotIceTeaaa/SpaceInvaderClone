using UnityEngine;

namespace SpaceInvader {
    public class EnemyBullet : MonoBehaviour {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;

        void Update() {
            _rb.linearVelocity = -transform.up * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("PlayerBullet")) {
                PoolManager.Instance.Return(BulletType.Enemy, gameObject);
            }

            else if (collision.CompareTag("Player")) {
                GameManager.Instance.HandlePlayerTakeDamage(_damage);
                PoolManager.Instance.Return(BulletType.Enemy, gameObject);
            }
        }
    }
}
