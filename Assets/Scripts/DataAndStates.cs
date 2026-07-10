using UnityEngine;

namespace SpaceInvader
{
    public class DataAndStates : MonoBehaviour
    {
        [Header("Game Related")]
        public bool isGameStart = false;
        public bool isGameOver = false;

        [Header("Player Related")]
        [SerializeField] public float shootCooldown;
        [SerializeField] public float maxHP;

        public Timer shootTimer;
        public bool canShoot;
        public float hp;
        public float score;

        public static DataAndStates Instance { get; private set; }
        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public void Initialize() {
            canShoot = true;
            shootTimer = new Timer(shootCooldown);
            hp = maxHP;
        }

        public void DecreasePlayerHP(float damage) {
            hp -= damage;
        }
    }
}
