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

        public void GameStart() {
            DataAndStates.Instance.isGameStart = true;
            DataAndStates.Instance.isGameOver = false;

            StartCoroutine(BeginGameplay());
        }

        private void Update() {
            if (DataAndStates.Instance.isGameStart) {
                HandleCanShoot();
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
                //game over
            } else {
                UI_Gameplay.Instance.UpdateHPBar();
            }
        }
    }
}
