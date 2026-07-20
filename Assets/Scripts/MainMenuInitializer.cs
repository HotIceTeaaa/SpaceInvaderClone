using UnityEngine;

namespace SpaceInvader
{
    public class MainMenuInitializer : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] public UI_Manager ui_mainMenu;


        public static MainMenuInitializer Instance { get; private set; }

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            //DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            ui_mainMenu.Initialize();
        }
    }
}
