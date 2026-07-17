using UnityEngine;


namespace SpaceInvader
{
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy Type")]
        [SerializeField] private EnemyType _type;

        [Header("Enemy Sprite Related")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _shooterSprite;
        [SerializeField] private Sprite _bomberSprite;
        [SerializeField] private Sprite _lasererSprite;

        [Header("Shooting Related")]
        [SerializeField] private float _shootCooldown;
        [SerializeField] private float _shootThreshold;

        [Header("Other")]
        [SerializeField] private float _scoreWorth;
        [SerializeField] private float _hp;

        private Timer _shootTimer;

        public void Initialize(EnemyType type) {
            _shootTimer = new Timer(_shootCooldown);

            switch (type) {
                case EnemyType.Shooter:
                    _spriteRenderer.sprite = _shooterSprite;

                    _shootCooldown = 1;
                    _shootThreshold = 0.5f;
                    _scoreWorth = 100;
                    _hp = 5;
                    break;

                case EnemyType.Bomber:
                    _spriteRenderer.sprite = _bomberSprite;

                    _shootCooldown = 1;
                    _shootThreshold = 0.5f;
                    _scoreWorth = 300;
                    _hp = 10;
                    break;

                case EnemyType.Laserer:
                    _spriteRenderer.sprite = _lasererSprite;

                    _shootCooldown = 1;
                    _shootThreshold = 0.5f;
                    _scoreWorth = 200;
                    _hp = 3;
                    break;
            }
        }

        private void Update() {
            if (DataAndStates.Instance.isGameStart) {
                _shootTimer.DecrementTimer();

                if (_shootTimer.isDone()) {
                    _shootTimer.ResetTimer();
                    AttemptShoot();
                }
            }
        }

        private void AttemptShoot() {
            if (Random.value < _shootThreshold) {
                Vector3 bulletSpawnPos = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
                PoolManager.Instance.GetAndSetPositionRotation(BulletType.Enemy, bulletSpawnPos, Quaternion.identity);
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
