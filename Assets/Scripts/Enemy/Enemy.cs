using UnityEngine;
using System.Collections;

namespace SpaceInvader
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _enemyBulletLifespan;
        [SerializeField] private float _shootCooldown;
        [SerializeField] private float _shootThreshold;

        [SerializeField] private float _scoreWorth;

        [SerializeField] private float _hp;

        private Timer _shootTimer;

        private void Start() {
           _shootTimer = new Timer(_shootCooldown);
        }

        private void Update() {
            _shootTimer.DecrementTimer();

            if (_shootTimer.isDone()) {
                _shootTimer.ResetTimer();
                AttemptShoot();
            }
        }

        private void AttemptShoot() {
            if (Random.value < _shootThreshold) {
                Vector3 bulletSpawnPos = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
                GameObject enemyBullet = PoolManager.Instance.GetAndSetPositionRotation(BulletType.Enemy, bulletSpawnPos, Quaternion.identity);

                StartCoroutine(ReturnAfter(BulletType.Enemy, enemyBullet, _enemyBulletLifespan));
            }
        }

        private IEnumerator ReturnAfter(BulletType type, GameObject obj, float lifespan) {
            yield return new WaitForSeconds(lifespan);

            if (obj.activeSelf) {
                PoolManager.Instance.Return(type, obj);
            }
        }

        public void HandleEnemyTakeDamage(float dmg) {
            _hp -= dmg;

            if (_hp <= 0f) {
                PoolManager.Instance.Return(gameObject);
                DataAndStates.Instance.score += _scoreWorth;
            }
        }
    }
}
