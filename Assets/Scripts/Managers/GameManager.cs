using Unity.VisualScripting;
using UnityEngine;

namespace SpaceInvader
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _playerObject;
        public static GameManager Instance { get; private set; }

        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable() {
            //EventManager.Instance.OnPlayerHit += HandlePlayerTakeDamage;
        }

        private void OnDisable() {
            //EventManager.Instance.OnPlayerHit -= HandlePlayerTakeDamage;
        }

        private void Update() {
            HandleCanShoot();
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
                Destroy(_playerObject);
                //game over
            } else {
                UI_Gameplay.Instance.UpdateHPBar();
            }
        }
    }
}
