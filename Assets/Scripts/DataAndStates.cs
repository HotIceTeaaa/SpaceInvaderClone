using UnityEngine;

namespace SpaceInvader
{
    public class DataAndStates : MonoBehaviour
    {
        [Header("Game Related")]
        public bool isGameStart = false;
        public bool isGameOver = false;

        [Header("Score Multiplier Related")]
        public float level;
        public float point;

        [Header("Other")]
        public Timer shootTimer;
        public bool canShoot;
        public float maxHP;
        public float hp;
        public float score;
        public int coin;


        public static DataAndStates Instance { get; private set; }
        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public void Initialize(PlayerShipSO playerShipSO) {
            canShoot = true;

            shootTimer = new Timer(playerShipSO.shootCooldown);
            maxHP = playerShipSO.maxHP;
            hp = playerShipSO.maxHP;

            score = 0;
        }

        public void DecreasePlayerHP(float damage) {
            hp -= damage;
        }

        public int CalculateAndSetCoin()
        {
            coin = (int) score / 5;
            return coin;
        }
    }
}
