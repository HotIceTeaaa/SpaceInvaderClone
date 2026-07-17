using UnityEngine;
using System.Collections;

namespace SpaceInvader
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private EnemySO[] _enemySOs;
        private EnemySO _selectedEnemySO;
        private Timer _shootTimer;
        private Transform[] _waypoints;
        private int _waypointIndex = 0;
        private float _speedMultiplier;

        public void Initialize(EnemyType type, Transform pathTransform, float speedMultiplier) {
            switch (type) {
                case EnemyType.Shooter:
                    _selectedEnemySO = _enemySOs[0];
                    break;

                case EnemyType.Bomber:
                    _selectedEnemySO = _enemySOs[1];
                    break;

                case EnemyType.Laserer:
                    _selectedEnemySO = _enemySOs[2];
                    break;
            }

            //init sprite
            _spriteRenderer.sprite = _selectedEnemySO.sprite;
            _shootTimer = new Timer(_selectedEnemySO.shootCooldown);

            //init speed multiplier 
            _speedMultiplier = speedMultiplier;

            //init waypoints 
            _waypoints = new Transform[pathTransform.childCount];
            for (int i = 0; i < pathTransform.childCount; i++) {
                _waypoints[i] = pathTransform.GetChild(i).transform;
            }
        }

        private void Update() {
            if (DataAndStates.Instance.isGameStart) {
                //follow waypoint
                FollowPath();

                //shoot
                _shootTimer.DecrementTimer();

                if (_shootTimer.isDone()) {
                    _shootTimer.ResetTimer();
                    AttemptShoot();
                }
            }
        }

        private void AttemptShoot() {
            if (Random.value < _selectedEnemySO.shootThreshold) {
                Vector3 bulletSpawnPos = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
                GameObject enemyBullet = PoolManager.Instance.GetAndSetPositionRotation(BulletType.Enemy, bulletSpawnPos, Quaternion.identity);

                StartCoroutine(ReturnAfter(BulletType.Enemy, enemyBullet, _selectedEnemySO.enemyBulletLifespan));
            }
        }

        private IEnumerator ReturnAfter(BulletType type, GameObject obj, float lifespan) {
            yield return new WaitForSeconds(lifespan);

            if (obj.activeSelf) {
                PoolManager.Instance.Return(type, obj);
            }
        }

        public void HandleEnemyTakeDamage(float dmg) {
            _selectedEnemySO.hp -= dmg;

            if (_selectedEnemySO.hp <= 0f) {
                PoolManager.Instance.Return(gameObject);
                DataAndStates.Instance.score += _selectedEnemySO.scoreWorth;
            }
        }

        void FollowPath() {
            if (_waypointIndex < _waypoints.Length) {
                Vector3 targetPosition = _waypoints[_waypointIndex].position;
                float moveDelta = _selectedEnemySO.speed * _speedMultiplier * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveDelta);

                if (transform.position == targetPosition) {
                    _waypointIndex++;
                }
            } else {
                PoolManager.Instance.Return(gameObject);
            }
        }
    }
}
