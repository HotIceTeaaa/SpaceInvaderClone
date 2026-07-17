using UnityEngine;

namespace SpaceInvader
{
    public class UI_Gameplay : MonoBehaviour
    {
   

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
        }

        private void BindObjects(GameObject endScreenPanel, GameObject hpFrontBar) {
            _endScreenPanel = Instantiate(endScreenPanel);
        }

        
    }
}
