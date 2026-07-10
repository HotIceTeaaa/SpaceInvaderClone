using UnityEngine;

namespace SpaceInvader
{
    public class UI_Gameplay : MonoBehaviour
    {
        [Header("Health Bar Related")]
        [SerializeField] private GameObject _hpFrontBar;
        private RectTransform _hpFrontBarRT;
        private float _hpFrontBarRTInitWidth;

        [Header("Game Over Screen Related")]
        [SerializeField] private GameObject _endScreenPanel;

        public static UI_Gameplay Instance { get; private set; }

        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }

        public void Initialize(GameObject endScreenPanel, GameObject hpFrontBar){
            BindObjects(endScreenPanel, hpFrontBar);
            _hpFrontBarRT = _hpFrontBar.GetComponent<RectTransform>();
            _hpFrontBarRTInitWidth = _hpFrontBarRT.sizeDelta.x;
        }

        private void BindObjects(GameObject endScreenPanel, GameObject hpFrontBar) {
            _hpFrontBar = Instantiate(hpFrontBar);
            _endScreenPanel = Instantiate(endScreenPanel);
        }

        public void UpdateHPBar() {
            float newWidth = _hpFrontBarRTInitWidth * DataAndStates.Instance.hp / DataAndStates.Instance.maxHP;
            Vector2 temp = new Vector2(newWidth, _hpFrontBarRT.sizeDelta.y);
            _hpFrontBarRT.sizeDelta = temp;
        }
    }
}
