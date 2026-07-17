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
        [SerializeField] public GameObject background;
        [SerializeField] public GameObject fog;
        [SerializeField] public UI_Gameplay ui_gameplay;
        [SerializeField] public UI_EndScreen ui_endScreen;
        [SerializeField] public EventSystem eventSystem;

        [Header("Core")]
        [SerializeField] public GameObject player;
        [SerializeField] public Player playerScript;
        [SerializeField] public DataAndStates dataAndStates;
        
        [Header("Managers")]
        [SerializeField] public GameManager gameManager;
        [SerializeField] public WaveManager waveManager;
        [SerializeField] public PoolManager poolManager;
        [SerializeField] public SceneHandler sceneHandler;
        [SerializeField] public PlayerPreferences prefs;

        [Header("Others")]
        

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

            dataAndStates.Initialize();
            playerScript.Initialize();
            gameManager.Initialize();
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
