using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

namespace SpaceInvader
{
    public class GameplayInitializer : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] new public Camera camera;
        [SerializeField] public Light2D globalLight;

        [Header("UI")]
        [SerializeField] public Canvas canvas;
        [SerializeField] public UI_Gameplay ui_gameplay;
        [SerializeField] public EventSystem eventSystem;

        [Header("Core")]
        [SerializeField] public GameObject player;
        [SerializeField] public DataAndStates dataAndStates;

        [Header("Managers")]
        [SerializeField] public GameManager gameManager;
        [SerializeField] public WaveManager waveManager;
        [SerializeField] public PoolManager poolManager;

        [Header("Others")]
        [SerializeField] public WaveSO[] waveSOs;
        [SerializeField] public GameObject endScreenPanel;
        [SerializeField] public GameObject hpFrontBar;

        public static GameplayInitializer Instance { get; private set; }

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            //DontDestroyOnLoad(gameObject);
        }

        private void Start() {
            BindObjects();

            foreach (WaveSO waveSO in waveSOs) {
                waveSO.Initialize();
            }

            dataAndStates.Initialize();
            gameManager.GameStart();
            ui_gameplay.Initialize(endScreenPanel, hpFrontBar);
        }

        private void BindObjects() {
            camera = Instantiate(camera);
            globalLight = Instantiate(globalLight);

            canvas = Instantiate(canvas);
            ui_gameplay = Instantiate(ui_gameplay);
            eventSystem = Instantiate(eventSystem);

            player = Instantiate(player);
            dataAndStates = Instantiate(dataAndStates);

            gameManager = Instantiate(gameManager);
            waveManager = Instantiate(waveManager);
            poolManager = Instantiate(poolManager);
        }
    }
}
