using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpaceInvader
{
    public class GameManager : MonoBehaviour
    {
        [Header("Wave Related")]
        [SerializeField] private WaveSO[] _waveSO;
        [SerializeField] private bool _isLooping;
        [SerializeField] private float _timeBetweenWaves;

        [Header("Player Related")]
        [SerializeField] private PlayerShipSO[] _playerShipSO;

        [Header("Score Multiplier Related")]
        [SerializeField] private float _increasePointBy;
        [SerializeField] private float _decreasePointBy;
        [SerializeField] public float _increaseLevelThreshold;

        [Header("Other")]
        public int currentWave;
        public static GameManager Instance { get; private set; }

        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        public void Initialize()
        {
            foreach (WaveSO waveSO in _waveSO) {
                waveSO.Initialize();
            }

            int i = PlayerPreferences.Instance.GetInt("shipType", 0);
            GameplayInitializer.Instance.playerScript.Initialize(_playerShipSO[i]);
            GameplayInitializer.Instance.playerBulletScript.Initialize(_playerShipSO[i]);
            DataAndStates.Instance.Initialize(_playerShipSO[i]);

            ResetScoreMultiplier();
            GameStart();
        }

        public void GameStart() {
            DataAndStates.Instance.isGameStart = true;
            DataAndStates.Instance.isGameOver = false;

            StartCoroutine(BeginGameplay());
        }

        public void GameOver() {
            DataAndStates.Instance.isGameStart = false;
            DataAndStates.Instance.isGameOver = true;
            
            AddAndSaveCoins();
            UI_Gameplay.Instance.PlayEndScreen();
        }

        private void Update() {
            if (DataAndStates.Instance.isGameStart) {
                HandleCanShoot();
                DecreaseScoreMultiplier();

                //update UI nya
                UI_Gameplay.Instance.UpdateScoreValue();
                UI_Gameplay.Instance.UpdateMultiplierValue();
                UI_Gameplay.Instance.UpdateGaugeValue();
            }
        }

        IEnumerator BeginGameplay() 
        {
            foreach (WaveSO wave in _waveSO) {
                currentWave = wave.waveNumber;
                StartCoroutine(WaveManager.Instance.BeginWave(wave));
                yield return new WaitForSeconds(_timeBetweenWaves);
            }
        }

        public void HandleCanShoot() {
            if (!DataAndStates.Instance.canShoot) {
                DataAndStates.Instance.shootTimer.DecrementTimer();

                if (DataAndStates.Instance.shootTimer.isDone()) {
                    DataAndStates.Instance.shootTimer.ResetTimer();
                    DataAndStates.Instance.canShoot = true;
                }
            }
        }

        public void HandlePlayerTakeDamage(float dmg) {
            DataAndStates.Instance.DecreasePlayerHP(dmg);

            if(DataAndStates.Instance.hp <= 0f) {
                Destroy(GameplayInitializer.Instance.player);
                GameOver();
            } else {
                UI_Gameplay.Instance.UpdateHPBar();
            }
        }

        private void AddAndSaveCoins(){
            int coin = DataAndStates.Instance.CalculateAndSetCoin();
            int coinFromPrefs = PlayerPreferences.Instance.GetInt("coin", -9999);

            coinFromPrefs += coin;
            PlayerPreferences.Instance.SaveInt("coin", coinFromPrefs);
        }

        public void IncreaseScoreMultiplier()
        {
            DataAndStates.Instance.point += _increasePointBy;

            if(DataAndStates.Instance.point >= _increaseLevelThreshold)
            {
                DataAndStates.Instance.level += 1;
                DataAndStates.Instance.point %= _increaseLevelThreshold;
            }
        }

        private void DecreaseScoreMultiplier()
        {
            DataAndStates.Instance.point -= _decreasePointBy * DataAndStates.Instance.level;

            if(DataAndStates.Instance.point <= 0f)
            {
                if(DataAndStates.Instance.level == 1)
                {
                    DataAndStates.Instance.point = 0;
                }
                else
                {
                    DataAndStates.Instance.level -= 1;
                    DataAndStates.Instance.point = _increaseLevelThreshold;
                }
            }
        }

        public void ResetScoreMultiplier()
        {
            DataAndStates.Instance.level = 1;
            DataAndStates.Instance.point = 0;

            UI_Gameplay.Instance.UpdateScoreValue();
            UI_Gameplay.Instance.UpdateMultiplierValue();
            UI_Gameplay.Instance.UpdateGaugeValue();
        }
    }
}
