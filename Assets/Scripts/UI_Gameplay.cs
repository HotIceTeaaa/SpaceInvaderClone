using UnityEngine;

namespace SpaceInvader
{
    public class UI_Gameplay : MonoBehaviour
    {
        [Header("Health Bar Related")]
        [SerializeField] private RectTransform _hpBarRT;
        private float _hpBarInitWidth;

        [Header("Game Over Screen Related")]
        [SerializeField] private GameObject _gameOverPanel;

        public static UI_Gameplay Instance { get; private set; }

        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            _hpBarInitWidth = _hpBarRT.sizeDelta.x;
        }

        public void UpdateHPBar() {
            float newWidth = _hpBarInitWidth * DataAndStates.Instance.hp / DataAndStates.Instance.maxHP;
            Vector2 temp = new Vector2(newWidth, _hpBarRT.sizeDelta.y);
            _hpBarRT.sizeDelta = temp;
        }
    }
}
