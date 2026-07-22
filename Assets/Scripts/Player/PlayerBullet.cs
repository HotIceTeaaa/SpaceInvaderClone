using UnityEngine;
using System.Collections;

namespace SpaceInvader
{
    public class PlayerBullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _playerBulletLifespan;

        public void OnEnable()
        {
            StartCoroutine(ReturnAfter(BulletType.Player, gameObject, _playerBulletLifespan));
        }

        public void Initialize(PlayerShipSO playerShipSO)
        {
            _speed = playerShipSO.speed;
            _damage = playerShipSO.damage;
            _playerBulletLifespan = playerShipSO.lifetime;
        }

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
                SFXManager.Instance.PlaySFX(SFX.ProjectileHit);
            }
        }

        private IEnumerator ReturnAfter(BulletType type, GameObject obj, float lifespan) {
            yield return new WaitForSeconds(lifespan);

            if (obj.activeSelf) {
                PoolManager.Instance.Return(type, obj);
            }
        }
    }
}
