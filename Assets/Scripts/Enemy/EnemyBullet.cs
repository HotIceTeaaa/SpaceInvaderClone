using UnityEngine;
using System.Collections;

namespace SpaceInvader {
    public class EnemyBullet : MonoBehaviour {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _enemyBulletLifespan;

        public void OnEnable()
        {
            StartCoroutine(ReturnAfter(BulletType.Enemy, gameObject, _enemyBulletLifespan));
        }

        void Update() {
            _rb.linearVelocity = -transform.up * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("PlayerBullet")) {
                PoolManager.Instance.Return(BulletType.Enemy, gameObject);
            }

            else if (collision.CompareTag("Player")) {
                GameManager.Instance.HandlePlayerTakeDamage(_damage);
                GameManager.Instance.ResetScoreMultiplier();

                PoolManager.Instance.Return(BulletType.Enemy, gameObject);
                GameplayInitializer.Instance.cameraShakeScript.Play();
            }
        }

        private IEnumerator ReturnAfter(BulletType type, GameObject obj, float lifespan)
        {
            yield return new WaitForSeconds(lifespan);

            if (obj.activeSelf)
            {
                PoolManager.Instance.Return(type, obj);
            }
        }
    }
}
